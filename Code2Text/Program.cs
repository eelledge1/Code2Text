using System.IO.Compression;
using System.Text.Json;
using System.Text;
using System.Collections.Concurrent;
using System.Xml.Linq;
using NLog;

public class Program
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    static void Main(string[] args)
    {
        // Load configuration
        string configPath = "code2text.json"; // Assume the configuration file is in the same directory as the executable
        Config config;

        try
        {
            string jsonString = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<Config>(jsonString);
            ValidateConfiguration(config);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load configuration: {ex.Message}");
            return;
        }

        ProcessSolution(config);
    }

    public static void ProcessSolution(Config config)
    {
        if (config.EnableLogging)
        {
            Logger.Info("Started processing solution at path: {0}", config.SolutionPath);
        }

        try
        {
            // Find all projects in the solution by looking for .csproj files
            var projectFiles = Directory.GetFiles(config.SolutionPath, "*.csproj", SearchOption.AllDirectories);
            if (projectFiles.Length == 0)
            {
                Console.WriteLine("No projects found in the specified solution path.");
                return;
            }

            int projectCount = projectFiles.Length;
            int processedProjects = 0;

            foreach (var projectFile in projectFiles)
            {
                string projectDirectory = Path.GetDirectoryName(projectFile);
                string projectName = Path.GetFileNameWithoutExtension(projectFile);

                StringBuilder sb = new StringBuilder();
                AppendProjectHeader(sb, projectName, config);

                if (config.IncludeProjectDependencies)
                {
                    AppendProjectDependencies(sb, projectFile);
                }

                var projectFilesToProcess = config.Extensions
                    .SelectMany(ext => Directory.GetFiles(projectDirectory, ext, SearchOption.AllDirectories))
                    .Where(file => !config.ExclusionFolders.Any(folder => file.Contains(folder)))
                    .ToArray();

                ConcurrentBag<string> fileContents = new ConcurrentBag<string>();

                if (config.EnableParallelProcessing)
                {
                    Parallel.ForEach(projectFilesToProcess, file =>
                    {
                        StringBuilder localSb = new StringBuilder();
                        AppendFileContent(localSb, file, config);
                        fileContents.Add(localSb.ToString());
                    });
                }
                else
                {
                    foreach (var file in projectFilesToProcess)
                    {
                        AppendFileContent(sb, file, config);
                    }
                }

                foreach (var content in fileContents)
                {
                    sb.Append(content);
                }

                string outputFilePath = config.SplitOutputByProject
                    ? Path.Combine(Path.GetDirectoryName(config.OutputPath), $"Code_Output_{projectName}.txt")
                    : config.OutputPath;

                if (config.SplitOutputByProject)
                {
                    File.WriteAllText(outputFilePath, sb.ToString(), Encoding.UTF8);
                }
                else
                {
                    File.AppendAllText(outputFilePath, sb.ToString(), Encoding.UTF8);
                }

                if (config.EnableProgressReporting)
                {
                    processedProjects++;
                    Console.WriteLine($"Progress: {processedProjects}/{projectCount} projects completed ({(processedProjects / (float)projectCount) * 100:F2}% done).");
                }

                if (config.EnableLogging)
                {
                    Logger.Info("Processed project: {0}", projectName);
                }
            }

            if (config.ZipOutput && config.SplitOutputByProject)
            {
                ZipOutputFiles(Path.GetDirectoryName(config.OutputPath), "*.txt", "Output.zip");
            }

            Console.WriteLine("Export completed successfully.");
            if (config.EnableLogging)
            {
                Logger.Info("Export completed successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            if (config.EnableLogging)
            {
                Logger.Error(ex, "An error occurred during processing");
            }
        }
    }

    private static void ValidateConfiguration(Config config)
    {
        if (!Directory.Exists(config.SolutionPath))
        {
            throw new DirectoryNotFoundException($"Solution path '{config.SolutionPath}' does not exist.");
        }
        if (string.IsNullOrWhiteSpace(config.OutputPath) || !Directory.Exists(Path.GetDirectoryName(config.OutputPath)))
        {
            throw new DirectoryNotFoundException($"Output path '{config.OutputPath}' is invalid.");
        }
    }

    private static void AppendProjectHeader(StringBuilder sb, string projectName, Config config)
    {
        if (config.MarkdownOutputFormatting)
        {
            sb.AppendLine($"## Project: {projectName}");
            sb.AppendLine();
        }
        else
        {
            sb.AppendLine($"# Project: {projectName}");
            sb.AppendLine(new string('-', 50));
        }
        sb.AppendLine();
    }

    private static void AppendProjectDependencies(StringBuilder sb, string projectFile)
    {
        try
        {
            var doc = XDocument.Load(projectFile);
            var dependencies = doc.Descendants("ProjectReference")
                .Select(elem => elem.Attribute("Include")?.Value)
                .Where(val => !string.IsNullOrEmpty(val))
                .Select(val => Path.GetFileNameWithoutExtension(val!))
                .ToList();

            if (dependencies.Any())
            {
                sb.AppendLine("### Project Dependencies:");
                foreach (var dep in dependencies)
                {
                    sb.AppendLine($"- {dep}");
                }
                sb.AppendLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not read dependencies for project {projectFile}: {ex.Message}");
            Logger.Warn(ex, "Failed to read project dependencies for {0}", projectFile);
        }
    }

    private static void AppendFileContent(StringBuilder sb, string file, Config config)
    {
        try
        {
            if (config.MarkdownOutputFormatting)
            {
                sb.AppendLine($"#### File: {Path.GetFileName(file)}");
                sb.AppendLine("```csharp");
            }
            else if (config.OutputFormatting)
            {
                sb.AppendLine($"//***********************************");
                sb.AppendLine($"// Filename: {Path.GetFileName(file)}");
                sb.AppendLine($"//***********************************");
            }

            int retries = 3;
            while (retries > 0)
            {
                try
                {
                    using (StreamReader reader = new StreamReader(file, Encoding.UTF8))
                    {
                        string? line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            sb.AppendLine(line);
                        }
                    }
                    break; // Success, break out of the retry loop
                }
                catch (IOException ex) when (retries > 0)
                {
                    retries--;
                    Console.WriteLine($"Retrying due to file access error: {file} - Retries left: {retries}");
                    Thread.Sleep(100); // Wait briefly before retrying
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not read file {file}: {ex.Message}");
                    Logger.Warn(ex, "Could not read file {0}", file);
                    break; // Exit on other exceptions
                }
            }

            if (config.MarkdownOutputFormatting)
            {
                sb.AppendLine("```");
                sb.AppendLine();
            }
            else
            {
                sb.AppendLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not read file {file}: {ex.Message}");
            Logger.Warn(ex, "Could not read file {0}", file);
        }
    }

    private static void ZipOutputFiles(string directoryPath, string filePattern, string outputZipFileName)
    {
        try
        {
            string zipPath = Path.Combine(directoryPath, outputZipFileName);
            using (FileStream zipToOpen = new FileStream(zipPath, FileMode.Create))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                {
                    var filesToZip = Directory.GetFiles(directoryPath, filePattern);
                    foreach (var file in filesToZip)
                    {
                        archive.CreateEntryFromFile(file, Path.GetFileName(file));
                    }
                }
            }
            Console.WriteLine("Output zipped successfully.");
            Logger.Info("Output zipped successfully to {0}", zipPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to zip output files: {ex.Message}");
            Logger.Error(ex, "Failed to zip output files");
        }
    }

    public class Config
    {
        public string SolutionPath { get; set; }
        public string OutputPath { get; set; }
        public string[] Extensions { get; set; }
        public string[] ExclusionFolders { get; set; }
        public bool EnableParallelProcessing { get; set; }
        public bool OutputFormatting { get; set; }
        public bool SplitOutputByProject { get; set; }
        public bool ZipOutput { get; set; }
        public bool EnableLogging { get; set; }
        public bool EnableProgressReporting { get; set; }
        public bool IncludeProjectDependencies { get; set; }
        public bool MarkdownOutputFormatting { get; set; }
    }
}
