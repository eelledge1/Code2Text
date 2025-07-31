namespace Code2Text.UI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtSolutionPath;
        private System.Windows.Forms.Button btnBrowseSolution;
        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.Button btnBrowseOutput;
        private System.Windows.Forms.TextBox txtExtensions;
        private System.Windows.Forms.TextBox txtExclusions;
        private System.Windows.Forms.CheckBox chkParallel;
        private System.Windows.Forms.CheckBox chkFormatting;
        private System.Windows.Forms.CheckBox chkSplit;
        private System.Windows.Forms.CheckBox chkZip;
        private System.Windows.Forms.CheckBox chkLogging;
        private System.Windows.Forms.CheckBox chkProgress;
        private System.Windows.Forms.CheckBox chkDependencies;
        private System.Windows.Forms.CheckBox chkMarkdown;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRun;

        private void InitializeComponent()
        {
            this.txtSolutionPath = new System.Windows.Forms.TextBox();
            this.btnBrowseSolution = new System.Windows.Forms.Button();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.btnBrowseOutput = new System.Windows.Forms.Button();
            this.txtExtensions = new System.Windows.Forms.TextBox();
            this.txtExclusions = new System.Windows.Forms.TextBox();
            this.chkParallel = new System.Windows.Forms.CheckBox();
            this.chkFormatting = new System.Windows.Forms.CheckBox();
            this.chkSplit = new System.Windows.Forms.CheckBox();
            this.chkZip = new System.Windows.Forms.CheckBox();
            this.chkLogging = new System.Windows.Forms.CheckBox();
            this.chkProgress = new System.Windows.Forms.CheckBox();
            this.chkDependencies = new System.Windows.Forms.CheckBox();
            this.chkMarkdown = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtSolutionPath
            // 
            this.txtSolutionPath.Location = new System.Drawing.Point(12, 12);
            this.txtSolutionPath.Size = new System.Drawing.Size(400, 23);
            // 
            // btnBrowseSolution
            // 
            this.btnBrowseSolution.Location = new System.Drawing.Point(418, 12);
            this.btnBrowseSolution.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseSolution.Text = "Browse";
            this.btnBrowseSolution.Click += new System.EventHandler(this.btnBrowseSolution_Click);
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Location = new System.Drawing.Point(12, 41);
            this.txtOutputPath.Size = new System.Drawing.Size(400, 23);
            // 
            // btnBrowseOutput
            // 
            this.btnBrowseOutput.Location = new System.Drawing.Point(418, 41);
            this.btnBrowseOutput.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseOutput.Text = "Output";
            this.btnBrowseOutput.Click += new System.EventHandler(this.btnBrowseOutput_Click);
            // 
            // txtExtensions
            // 
            this.txtExtensions.Location = new System.Drawing.Point(12, 70);
            this.txtExtensions.Size = new System.Drawing.Size(481, 23);
            this.txtExtensions.PlaceholderText = "Extensions (; separated)";
            // 
            // txtExclusions
            // 
            this.txtExclusions.Location = new System.Drawing.Point(12, 99);
            this.txtExclusions.Size = new System.Drawing.Size(481, 23);
            this.txtExclusions.PlaceholderText = "Exclusion Folders (; separated)";
            // 
            // chkParallel
            // 
            this.chkParallel.Location = new System.Drawing.Point(12, 128);
            this.chkParallel.Size = new System.Drawing.Size(150, 19);
            this.chkParallel.Text = "Parallel Processing";
            // 
            // chkFormatting
            // 
            this.chkFormatting.Location = new System.Drawing.Point(168, 128);
            this.chkFormatting.Size = new System.Drawing.Size(120, 19);
            this.chkFormatting.Text = "Output Formatting";
            // 
            // chkSplit
            // 
            this.chkSplit.Location = new System.Drawing.Point(294, 128);
            this.chkSplit.Size = new System.Drawing.Size(110, 19);
            this.chkSplit.Text = "Split by Project";
            // 
            // chkZip
            // 
            this.chkZip.Location = new System.Drawing.Point(410, 128);
            this.chkZip.Size = new System.Drawing.Size(80, 19);
            this.chkZip.Text = "Zip Output";
            // 
            // chkLogging
            // 
            this.chkLogging.Location = new System.Drawing.Point(12, 153);
            this.chkLogging.Size = new System.Drawing.Size(80, 19);
            this.chkLogging.Text = "Logging";
            // 
            // chkProgress
            // 
            this.chkProgress.Location = new System.Drawing.Point(98, 153);
            this.chkProgress.Size = new System.Drawing.Size(95, 19);
            this.chkProgress.Text = "Show Progress";
            // 
            // chkDependencies
            // 
            this.chkDependencies.Location = new System.Drawing.Point(199, 153);
            this.chkDependencies.Size = new System.Drawing.Size(158, 19);
            this.chkDependencies.Text = "Include Dependencies";
            // 
            // chkMarkdown
            // 
            this.chkMarkdown.Location = new System.Drawing.Point(363, 153);
            this.chkMarkdown.Size = new System.Drawing.Size(130, 19);
            this.chkMarkdown.Text = "Markdown Format";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 182);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(93, 182);
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.Text = "Run";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 218);
            this.Controls.Add(this.txtSolutionPath);
            this.Controls.Add(this.btnBrowseSolution);
            this.Controls.Add(this.txtOutputPath);
            this.Controls.Add(this.btnBrowseOutput);
            this.Controls.Add(this.txtExtensions);
            this.Controls.Add(this.txtExclusions);
            this.Controls.Add(this.chkParallel);
            this.Controls.Add(this.chkFormatting);
            this.Controls.Add(this.chkSplit);
            this.Controls.Add(this.chkZip);
            this.Controls.Add(this.chkLogging);
            this.Controls.Add(this.chkProgress);
            this.Controls.Add(this.chkDependencies);
            this.Controls.Add(this.chkMarkdown);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnRun);
            this.Text = "Code2Text Config";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
