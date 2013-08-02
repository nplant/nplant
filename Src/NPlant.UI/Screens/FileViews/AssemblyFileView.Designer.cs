namespace NPlant.UI
{
    partial class AssemblyFileView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ImageGenerationSummaryControl = new NPlant.UI.Screens.Controls.ImageGenerationSummary();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.DiagramsListBox = new System.Windows.Forms.ListBox();
            this.GenerateOnSelectionChangedCheckBox = new System.Windows.Forms.CheckBox();
            this.GenerateButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ImageGenerationSummaryControl
            // 
            this.ImageGenerationSummaryControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImageGenerationSummaryControl.Location = new System.Drawing.Point(3, 0);
            this.ImageGenerationSummaryControl.Name = "ImageGenerationSummaryControl";
            this.ImageGenerationSummaryControl.Size = new System.Drawing.Size(725, 735);
            this.ImageGenerationSummaryControl.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.DiagramsListBox);
            this.splitContainer1.Panel1.Controls.Add(this.GenerateOnSelectionChangedCheckBox);
            this.splitContainer1.Panel1.Controls.Add(this.GenerateButton);
            this.splitContainer1.Panel1MinSize = 252;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ImageGenerationSummaryControl);
            this.splitContainer1.Size = new System.Drawing.Size(1039, 735);
            this.splitContainer1.SplitterDistance = 304;
            this.splitContainer1.TabIndex = 1;
            // 
            // DiagramsListBox
            // 
            this.DiagramsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DiagramsListBox.FormattingEnabled = true;
            this.DiagramsListBox.Location = new System.Drawing.Point(3, 3);
            this.DiagramsListBox.Name = "DiagramsListBox";
            this.DiagramsListBox.Size = new System.Drawing.Size(298, 693);
            this.DiagramsListBox.TabIndex = 5;
            this.DiagramsListBox.SelectedIndexChanged += new System.EventHandler(this.OnDiagramSelectionChanged);
            // 
            // GenerateOnSelectionChangedCheckBox
            // 
            this.GenerateOnSelectionChangedCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GenerateOnSelectionChangedCheckBox.AutoSize = true;
            this.GenerateOnSelectionChangedCheckBox.Location = new System.Drawing.Point(3, 711);
            this.GenerateOnSelectionChangedCheckBox.Name = "GenerateOnSelectionChangedCheckBox";
            this.GenerateOnSelectionChangedCheckBox.Size = new System.Drawing.Size(147, 17);
            this.GenerateOnSelectionChangedCheckBox.TabIndex = 4;
            this.GenerateOnSelectionChangedCheckBox.Text = "Regenerate Automatically";
            this.GenerateOnSelectionChangedCheckBox.UseVisualStyleBackColor = true;
            // 
            // GenerateButton
            // 
            this.GenerateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GenerateButton.Enabled = false;
            this.GenerateButton.Location = new System.Drawing.Point(226, 707);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(75, 23);
            this.GenerateButton.TabIndex = 3;
            this.GenerateButton.Text = "Generate";
            this.GenerateButton.UseVisualStyleBackColor = true;
            this.GenerateButton.Click += new System.EventHandler(this.OnGenerateButtonClick);
            // 
            // AssemblyFileView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "AssemblyFileView";
            this.Size = new System.Drawing.Size(1039, 735);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Screens.Controls.ImageGenerationSummary ImageGenerationSummaryControl;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox DiagramsListBox;
        private System.Windows.Forms.CheckBox GenerateOnSelectionChangedCheckBox;
        private System.Windows.Forms.Button GenerateButton;
    }
}
