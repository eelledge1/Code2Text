using System.Text;

class Program
{
    static void Main(string[] args)
    {
        //Solution path
        string solutionPath = @"C:\Users\eelle\source\repos\HomeHarmony";
        ////Output path
        string outputPath = @"C:\Users\eelle\source\repos\HomeHarmony\Code_Output.txt";

        string[] extensions = { "*.cs", "*.razor", "*.css" };
        var files = extensions.SelectMany(ext => Directory.GetFiles(solutionPath, ext, SearchOption.AllDirectories)).ToArray();

        StringBuilder sb = new StringBuilder();

        foreach (var file in files)
        {
            sb.AppendLine($"//***********************************");
            sb.AppendLine($"//Filename: {Path.GetFileName(file)}");
            sb.AppendLine($"//***********************************");
            sb.AppendLine(File.ReadAllText(file));
            sb.AppendLine();
        }

        File.WriteAllText(outputPath, sb.ToString());

        Console.WriteLine("Export completed successfully.");
    }
}