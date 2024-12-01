# Shn2Txt / Code2Text

Welcome to **Shn2Txt / Code2Text**, a powerful and flexible tool designed for developers who need to extract and organize code from multi-project solutions. The application simplifies the process of aggregating code files from a solution into a well-organized, readable text format, with options to customize output based on specific needs. Whether you're doing code review, documentation, or providing detailed context to an AI platform like ChatGPT for better understanding, **Shn2Txt / Code2Text** has you covered.

## Purpose

The purpose of **Shn2Txt / Code2Text** is to make it easy for developers to gather all source code files from a multi-project solution into a structured output. The tool reads through different projects, collects code files, organizes them, and outputs them in a readable format (e.g., Markdown or plain text). It also supports features like project dependency mapping, file zipping, and parallel processing for enhanced efficiency.

This tool is especially useful for:
- **AI Context and Prompt Engineering**: Generate structured code and project context for platforms like ChatGPT, allowing for better understanding and more precise prompt engineering.
- **Code reviews**: Get an overview of the entire solution in a consolidated format.
- **Archiving**: Store code snapshots in text form for easy version tracking.
- **Documentation**: Generate well-organized, formatted text outputs that can be used for documentation purposes.

## Features

**Shn2Txt / Code2Text** comes packed with features designed to streamline code extraction and output:

- **Solution Path Configuration**: Easily point to any solution, and the tool will recursively gather all source code files.
- **Project-Level Organization**: Organizes output by projects and includes headers for easy navigation.
- **Supports Multiple File Extensions**: Collects files like `.cs`, `.razor`, `.css`, etc., and allows extension customization.
- **Markdown and Plain Text Output**: Generate code with Markdown-like structure for readability or plain text for simplicity.
- **Multi-Output Options**: Create separate outputs for each project or combine everything into a single consolidated file.
- **Project Dependencies Inclusion**: Automatically includes project references and dependencies in the output.
- **Parallel Processing**: Supports parallel processing for faster extraction in large projects.
- **Zipped Output**: Optionally zip output files for easy sharing and storage.
- **Logging and Progress Reporting**: Logs critical operations and reports progress during extraction.
- **Retry Mechanism for File Access**: Handles temporarily locked files with retry logic for robustness.

## How to Use

### 1. Configuration

The tool relies on a JSON configuration file named `code2text.json` for its setup. Here is an example of what the configuration file looks like:

```json
{
  "SolutionPath": "C:\Path\To\Your\Solution",
  "OutputPath": "C:\Path\To\Output\Code_Output.txt",
  "Extensions": [ "*.cs", "*.razor", "*.css" ],
  "ExclusionFolders": [ "\bin\", "\obj\", "\Migrations\" ],
  "EnableParallelProcessing": true,
  "OutputFormatting": true,
  "SplitOutputByProject": true,
  "ZipOutput": true,
  "EnableLogging": true,
  "EnableProgressReporting": true,
  "IncludeProjectDependencies": true,
  "MarkdownOutputFormatting": true
}
```

### 2. Setting Up the Configuration

- **SolutionPath**: Path to the solution directory that contains the projects you want to extract code from.
- **OutputPath**: Path to the output file where the results will be saved. If `SplitOutputByProject` is enabled, each project will have its own output file.
- **Extensions**: List of file types/extensions to include, e.g., `*.cs` for C# files, `*.razor` for Blazor components.
- **ExclusionFolders**: List of folders to be excluded from extraction, like build directories (`bin`, `obj`).
- **EnableParallelProcessing**: Enable/disable parallel processing to speed up extraction.
- **OutputFormatting**: Enable for additional output formatting, such as file headers.
- **SplitOutputByProject**: Output each project to a separate file.
- **ZipOutput**: If enabled, the output files will be zipped.
- **EnableLogging**: Enable logging to track progress and errors.
- **EnableProgressReporting**: Report progress to the console during extraction.
- **IncludeProjectDependencies**: Includes a list of project dependencies in the output.
- **MarkdownOutputFormatting**: Use Markdown formatting for more readable outputs.

### 3. Running the Tool

1. **Prepare the Configuration File**: Adjust `code2text.json` as per your requirements.
2. **Run the Application**:
   - Run the compiled executable.
   - Alternatively, if using the C# source, run it directly in your IDE or with `dotnet run`.
3. **Output Files**:
   - Depending on your configuration, you will find the output in the specified path either as separate files (by project) or a single combined file.
4. **Zipping Output** (Optional): If enabled, output files will be compressed into `Output.zip` for convenience.

## Example Output

Below is an example of how the generated output may look:

```
## Project: ProjectName.Core

### Project Dependencies:
- ProjectName.Shared

#### File: ExampleClass.cs
```csharp
public class ExampleClass
{
    public int Id { get; set; }
    public string Name { get; set; }
}
```

==================================================

## Project: ProjectName.UI

### Project Dependencies:
- ProjectName.Core

#### File: AnotherComponent.razor
```csharp
<h3>Hello, ProjectName!</h3>
```
```

## Contributing

Contributions are welcome! If you find a bug or have an idea for a feature, feel free to open an issue or submit a pull request. Please follow the contribution guidelines to ensure smooth collaboration.

### Contribution Guidelines
- **Issues**: Open an issue to discuss bugs, features, or enhancements.
- **Fork & PR**: Fork the repository, make changes, and submit a pull request.
- **Code Style**: Follow existing code conventions to maintain consistency.

## License

This project is licensed under the MIT License. Feel free to use it, modify it, and share it as you see fit.

## Support

If you encounter any issues or need help getting started, please open an issue on the GitHub repository. Contributions to improve the tool are greatly appreciated!

## Future Features

- **Web Interface**: Provide a simple web-based interface for selecting solutions and generating outputs.
- **Code Highlighting**: Support for syntax highlighting in generated Markdown files.
- **Plugin System**: Add extensibility for supporting more languages and custom extraction rules.

---
We hope **Shn2Txt / Code2Text** proves valuable to your development workflows and helps you provide well-structured context to AI platforms for better interaction. If you have any feedback or suggestions, feel free to reach out. Happy coding!

