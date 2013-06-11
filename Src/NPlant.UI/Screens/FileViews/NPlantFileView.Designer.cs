namespace NPlant.UI.Screens.FileViews
{
    partial class NPlantFileView
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.GenerateOnTextChangeCheckBox = new System.Windows.Forms.CheckBox();
            this.GenerateButton = new System.Windows.Forms.Button();
            this.CopyButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.DiagramTextTextBox = new System.Windows.Forms.RichTextBox();
            this.ImageGenerationSummaryControl = new NPlant.UI.Screens.Controls.ImageGenerationSummary();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.GenerateOnTextChangeCheckBox);
            this.splitContainer1.Panel1.Controls.Add(this.GenerateButton);
            this.splitContainer1.Panel1.Controls.Add(this.CopyButton);
            this.splitContainer1.Panel1.Controls.Add(this.SaveButton);
            this.splitContainer1.Panel1.Controls.Add(this.DiagramTextTextBox);
            this.splitContainer1.Panel1MinSize = 252;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ImageGenerationSummaryControl);
            this.splitContainer1.Size = new System.Drawing.Size(1002, 688);
            this.splitContainer1.SplitterDistance = 294;
            this.splitContainer1.TabIndex = 0;
            // 
            // GenerateOnTextChangeCheckBox
            // 
            this.GenerateOnTextChangeCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GenerateOnTextChangeCheckBox.AutoSize = true;
            this.GenerateOnTextChangeCheckBox.Location = new System.Drawing.Point(3, 642);
            this.GenerateOnTextChangeCheckBox.Name = "GenerateOnTextChangeCheckBox";
            this.GenerateOnTextChangeCheckBox.Size = new System.Drawing.Size(114, 17);
            this.GenerateOnTextChangeCheckBox.TabIndex = 4;
            this.GenerateOnTextChangeCheckBox.Text = "Regenerate Image";
            this.GenerateOnTextChangeCheckBox.UseVisualStyleBackColor = true;
            this.GenerateOnTextChangeCheckBox.CheckedChanged += new System.EventHandler(this.OnGenerateOnTextChangeCheckChanged);
            // 
            // GenerateButton
            // 
            this.GenerateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GenerateButton.Location = new System.Drawing.Point(216, 660);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(75, 23);
            this.GenerateButton.TabIndex = 3;
            this.GenerateButton.Text = "Generate";
            this.GenerateButton.UseVisualStyleBackColor = true;
            this.GenerateButton.Click += new System.EventHandler(this.OnGenerateButtonClick);
            // 
            // CopyButton
            // 
            this.CopyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CopyButton.Location = new System.Drawing.Point(84, 660);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(75, 23);
            this.CopyButton.TabIndex = 2;
            this.CopyButton.Text = "Copy";
            this.CopyButton.UseVisualStyleBackColor = true;
            this.CopyButton.Click += new System.EventHandler(this.OnCopyButtonClick);
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SaveButton.Location = new System.Drawing.Point(3, 660);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.OnSaveButtonClick);
            // 
            // DiagramTextTextBox
            // 
            this.DiagramTextTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DiagramTextTextBox.Location = new System.Drawing.Point(3, 3);
            this.DiagramTextTextBox.Name = "DiagramTextTextBox";
            this.DiagramTextTextBox.Size = new System.Drawing.Size(288, 636);
            this.DiagramTextTextBox.TabIndex = 0;
            this.DiagramTextTextBox.Text = "";
            this.DiagramTextTextBox.WordWrap = false;
            // 
            // ImageGenerationSummaryControl
            // 
            this.ImageGenerationSummaryControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImageGenerationSummaryControl.Location = new System.Drawing.Point(3, 0);
            this.ImageGenerationSummaryControl.Name = "ImageGenerationSummaryControl";
            this.ImageGenerationSummaryControl.Size = new System.Drawing.Size(698, 688);
            this.ImageGenerationSummaryControl.TabIndex = 1;
            // 
            // NPlantFileView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "NPlantFileView";
            this.Size = new System.Drawing.Size(1002, 688);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox DiagramTextTextBox;
        private Controls.ImageGenerationSummary ImageGenerationSummaryControl;
        private System.Windows.Forms.Button GenerateButton;
        private System.Windows.Forms.Button CopyButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.CheckBox GenerateOnTextChangeCheckBox;
    }
}
