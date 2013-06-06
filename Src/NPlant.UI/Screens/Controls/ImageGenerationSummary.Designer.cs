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
            this.PictureBoxPanel = new System.Windows.Forms.Panel();
            this.DiagramViewerPictureBox = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.NormalSizeModel = new System.Windows.Forms.RadioButton();
            this.ZoomSizeMode = new System.Windows.Forms.RadioButton();
            this.StretchImageSizeMode = new System.Windows.Forms.RadioButton();
            this.AutoSizeSizeMode = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.PictureBoxPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DiagramViewerPictureBox)).BeginInit();
            this.panel1.SuspendLayout();
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
            // PictureBoxPanel
            // 
            this.PictureBoxPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureBoxPanel.AutoScroll = true;
            this.PictureBoxPanel.Controls.Add(this.panel1);
            this.PictureBoxPanel.Controls.Add(this.DiagramViewerPictureBox);
            this.PictureBoxPanel.Location = new System.Drawing.Point(6, 3);
            this.PictureBoxPanel.Name = "PictureBoxPanel";
            this.PictureBoxPanel.Size = new System.Drawing.Size(498, 383);
            this.PictureBoxPanel.TabIndex = 2;
            // 
            // DiagramViewerPictureBox
            // 
            this.DiagramViewerPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DiagramViewerPictureBox.BackColor = System.Drawing.Color.White;
            this.DiagramViewerPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DiagramViewerPictureBox.Location = new System.Drawing.Point(3, 32);
            this.DiagramViewerPictureBox.Name = "DiagramViewerPictureBox";
            this.DiagramViewerPictureBox.Size = new System.Drawing.Size(492, 348);
            this.DiagramViewerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.DiagramViewerPictureBox.TabIndex = 4;
            this.DiagramViewerPictureBox.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.AutoSizeSizeMode);
            this.panel1.Controls.Add(this.StretchImageSizeMode);
            this.panel1.Controls.Add(this.ZoomSizeMode);
            this.panel1.Controls.Add(this.NormalSizeModel);
            this.panel1.Location = new System.Drawing.Point(3, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(492, 26);
            this.panel1.TabIndex = 7;
            // 
            // NormalSizeModel
            // 
            this.NormalSizeModel.AutoSize = true;
            this.NormalSizeModel.Location = new System.Drawing.Point(3, 3);
            this.NormalSizeModel.Name = "NormalSizeModel";
            this.NormalSizeModel.Size = new System.Drawing.Size(58, 17);
            this.NormalSizeModel.TabIndex = 6;
            this.NormalSizeModel.Tag = "";
            this.NormalSizeModel.Text = "Normal";
            this.NormalSizeModel.UseVisualStyleBackColor = true;
            this.NormalSizeModel.CheckedChanged += new System.EventHandler(this.SizeModeChanged);
            // 
            // ZoomSizeMode
            // 
            this.ZoomSizeMode.AutoSize = true;
            this.ZoomSizeMode.Checked = true;
            this.ZoomSizeMode.Location = new System.Drawing.Point(359, 2);
            this.ZoomSizeMode.Name = "ZoomSizeMode";
            this.ZoomSizeMode.Size = new System.Drawing.Size(52, 17);
            this.ZoomSizeMode.TabIndex = 7;
            this.ZoomSizeMode.Tag = "";
            this.ZoomSizeMode.Text = "Zoom";
            this.ZoomSizeMode.UseVisualStyleBackColor = true;
            this.ZoomSizeMode.CheckedChanged += new System.EventHandler(this.SizeModeChanged);
            // 
            // StretchImageSizeMode
            // 
            this.StretchImageSizeMode.AutoSize = true;
            this.StretchImageSizeMode.Location = new System.Drawing.Point(71, 3);
            this.StretchImageSizeMode.Name = "StretchImageSizeMode";
            this.StretchImageSizeMode.Size = new System.Drawing.Size(88, 17);
            this.StretchImageSizeMode.TabIndex = 8;
            this.StretchImageSizeMode.Tag = "";
            this.StretchImageSizeMode.Text = "StretchImage";
            this.StretchImageSizeMode.UseVisualStyleBackColor = true;
            this.StretchImageSizeMode.CheckedChanged += new System.EventHandler(this.SizeModeChanged);
            // 
            // AutoSizeSizeMode
            // 
            this.AutoSizeSizeMode.AutoSize = true;
            this.AutoSizeSizeMode.Location = new System.Drawing.Point(169, 2);
            this.AutoSizeSizeMode.Name = "AutoSizeSizeMode";
            this.AutoSizeSizeMode.Size = new System.Drawing.Size(67, 17);
            this.AutoSizeSizeMode.TabIndex = 9;
            this.AutoSizeSizeMode.Tag = "";
            this.AutoSizeSizeMode.Text = "AutoSize";
            this.AutoSizeSizeMode.UseVisualStyleBackColor = true;
            this.AutoSizeSizeMode.CheckedChanged += new System.EventHandler(this.SizeModeChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(253, 2);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(85, 17);
            this.radioButton2.TabIndex = 10;
            this.radioButton2.Tag = "";
            this.radioButton2.Text = "CenterImage";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.SizeModeChanged);
            // 
            // ImageGenerationSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PictureBoxPanel);
            this.Controls.Add(this.LoadedFilePathTextBox);
            this.Controls.Add(this.GenerationProgressBar);
            this.Name = "ImageGenerationSummary";
            this.Size = new System.Drawing.Size(507, 415);
            this.PictureBoxPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DiagramViewerPictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar GenerationProgressBar;
        private System.Windows.Forms.TextBox LoadedFilePathTextBox;
        private System.Windows.Forms.Panel PictureBoxPanel;
        private System.Windows.Forms.PictureBox DiagramViewerPictureBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton ZoomSizeMode;
        private System.Windows.Forms.RadioButton NormalSizeModel;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton AutoSizeSizeMode;
        private System.Windows.Forms.RadioButton StretchImageSizeMode;
    }
}
