namespace NPlant.UI.Screens.Controls
{
    partial class ImageGenerationSummary
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
            this.GenerationProgressBar = new System.Windows.Forms.ProgressBar();
            this.LoadedFilePathTextBox = new System.Windows.Forms.TextBox();
            this.DiagramViewerPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.DiagramViewerPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // GenerationProgressBar
            // 
            this.GenerationProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GenerationProgressBar.Location = new System.Drawing.Point(3, 392);
            this.GenerationProgressBar.Name = "GenerationProgressBar";
            this.GenerationProgressBar.Size = new System.Drawing.Size(504, 23);
            this.GenerationProgressBar.TabIndex = 0;
            // 
            // LoadedFilePathTextBox
            // 
            this.LoadedFilePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadedFilePathTextBox.Location = new System.Drawing.Point(6, 392);
            this.LoadedFilePathTextBox.Name = "LoadedFilePathTextBox";
            this.LoadedFilePathTextBox.ReadOnly = true;
            this.LoadedFilePathTextBox.Size = new System.Drawing.Size(498, 20);
            this.LoadedFilePathTextBox.TabIndex = 1;
            this.LoadedFilePathTextBox.Visible = false;
            // 
            // DiagramViewerPictureBox
            // 
            this.DiagramViewerPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DiagramViewerPictureBox.BackColor = System.Drawing.Color.White;
            this.DiagramViewerPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DiagramViewerPictureBox.Location = new System.Drawing.Point(3, 3);
            this.DiagramViewerPictureBox.Name = "DiagramViewerPictureBox";
            this.DiagramViewerPictureBox.Size = new System.Drawing.Size(504, 383);
            this.DiagramViewerPictureBox.TabIndex = 2;
            this.DiagramViewerPictureBox.TabStop = false;
            // 
            // ImageGenerationSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DiagramViewerPictureBox);
            this.Controls.Add(this.LoadedFilePathTextBox);
            this.Controls.Add(this.GenerationProgressBar);
            this.Name = "ImageGenerationSummary";
            this.Size = new System.Drawing.Size(507, 415);
            ((System.ComponentModel.ISupportInitialize)(this.DiagramViewerPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar GenerationProgressBar;
        private System.Windows.Forms.TextBox LoadedFilePathTextBox;
        private System.Windows.Forms.PictureBox DiagramViewerPictureBox;
    }
}
