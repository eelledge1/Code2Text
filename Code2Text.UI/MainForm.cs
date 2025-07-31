using System;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using Code2Text; // to access Program and Config

namespace Code2Text.UI
{
    public partial class MainForm : Form
    {
        private Code2Text.Program.Config _config = new Code2Text.Program.Config();
        private readonly string _configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "code2text.json");

        public MainForm()
        {
            InitializeComponent();
            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            if (File.Exists(_configPath))
            {
                try
                {
                    string jsonString = File.ReadAllText(_configPath);
                    _config = JsonSerializer.Deserialize<Code2Text.Program.Config>(jsonString) ?? new Code2Text.Program.Config();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load configuration: {ex.Message}");
                    _config = new Code2Text.Program.Config();
                }
            }
            BindConfigToControls();
        }

        private void SaveConfiguration()
        {
            BindControlsToConfig();
            string jsonString = JsonSerializer.Serialize(_config, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_configPath, jsonString);
        }

        private void BindConfigToControls()
        {
            txtSolutionPath.Text = _config.SolutionPath ?? string.Empty;
            txtOutputPath.Text = _config.OutputPath ?? string.Empty;
            txtExtensions.Text = _config.Extensions != null ? string.Join(';', _config.Extensions) : string.Empty;
            txtExclusions.Text = _config.ExclusionFolders != null ? string.Join(';', _config.ExclusionFolders) : string.Empty;
            chkParallel.Checked = _config.EnableParallelProcessing;
            chkFormatting.Checked = _config.OutputFormatting;
            chkSplit.Checked = _config.SplitOutputByProject;
            chkZip.Checked = _config.ZipOutput;
            chkLogging.Checked = _config.EnableLogging;
            chkProgress.Checked = _config.EnableProgressReporting;
            chkDependencies.Checked = _config.IncludeProjectDependencies;
            chkMarkdown.Checked = _config.MarkdownOutputFormatting;
        }

        private void BindControlsToConfig()
        {
            _config.SolutionPath = txtSolutionPath.Text;
            _config.OutputPath = txtOutputPath.Text;
            _config.Extensions = txtExtensions.Text.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            _config.ExclusionFolders = txtExclusions.Text.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            _config.EnableParallelProcessing = chkParallel.Checked;
            _config.OutputFormatting = chkFormatting.Checked;
            _config.SplitOutputByProject = chkSplit.Checked;
            _config.ZipOutput = chkZip.Checked;
            _config.EnableLogging = chkLogging.Checked;
            _config.EnableProgressReporting = chkProgress.Checked;
            _config.IncludeProjectDependencies = chkDependencies.Checked;
            _config.MarkdownOutputFormatting = chkMarkdown.Checked;
        }

        private void btnBrowseSolution_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog();
            dialog.Filter = "Solution or Project (*.sln;*.csproj)|*.sln;*.csproj";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtSolutionPath.Text = dialog.FileName;
            }
        }

        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            using var dialog = new SaveFileDialog();
            dialog.Filter = "Text Files (*.txt)|*.txt";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtOutputPath.Text = dialog.FileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveConfiguration();
            MessageBox.Show("Configuration saved.");
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            SaveConfiguration();
            try
            {
                Code2Text.Program.ProcessSolution(_config);
                MessageBox.Show("Processing completed.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}
