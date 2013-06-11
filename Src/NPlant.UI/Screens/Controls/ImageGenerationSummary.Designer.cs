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
            this.components = new System.ComponentModel.Container();
            this.PictureBoxPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.AutoSizeSizeMode = new System.Windows.Forms.RadioButton();
            this.StretchImageSizeMode = new System.Windows.Forms.RadioButton();
            this.ZoomSizeMode = new System.Windows.Forms.RadioButton();
            this.NormalSizeModel = new System.Windows.Forms.RadioButton();
            this.DiagramViewerPictureBox = new System.Windows.Forms.PictureBox();
            this.PictureBoxContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveImageToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PictureBoxPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DiagramViewerPictureBox)).BeginInit();
            this.PictureBoxContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
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
            this.PictureBoxPanel.Size = new System.Drawing.Size(498, 409);
            this.PictureBoxPanel.TabIndex = 2;
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
            // ZoomSizeMode
            // 
            this.ZoomSizeMode.AutoSize = true;
            this.ZoomSizeMode.Checked = true;
            this.ZoomSizeMode.Location = new System.Drawing.Point(359, 2);
            this.ZoomSizeMode.Name = "ZoomSizeMode";
            this.ZoomSizeMode.Size = new System.Drawing.Size(52, 17);
            this.ZoomSizeMode.TabIndex = 7;
            this.ZoomSizeMode.TabStop = true;
            this.ZoomSizeMode.Tag = "";
            this.ZoomSizeMode.Text = "Zoom";
            this.ZoomSizeMode.UseVisualStyleBackColor = true;
            this.ZoomSizeMode.CheckedChanged += new System.EventHandler(this.SizeModeChanged);
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
            // DiagramViewerPictureBox
            // 
            this.DiagramViewerPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DiagramViewerPictureBox.BackColor = System.Drawing.Color.White;
            this.DiagramViewerPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DiagramViewerPictureBox.ContextMenuStrip = this.PictureBoxContextMenuStrip;
            this.DiagramViewerPictureBox.Location = new System.Drawing.Point(3, 32);
            this.DiagramViewerPictureBox.Name = "DiagramViewerPictureBox";
            this.DiagramViewerPictureBox.Size = new System.Drawing.Size(492, 374);
            this.DiagramViewerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.DiagramViewerPictureBox.TabIndex = 4;
            this.DiagramViewerPictureBox.TabStop = false;
            // 
            // PictureBoxContextMenuStrip
            // 
            this.PictureBoxContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveImageToFileToolStripMenuItem});
            this.PictureBoxContextMenuStrip.Name = "PictureBoxContextMenuStrip";
            this.PictureBoxContextMenuStrip.Size = new System.Drawing.Size(179, 26);
            this.PictureBoxContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.OnPictureBoxContextMenuStripOpening);
            // 
            // saveImageToFileToolStripMenuItem
            // 
            this.saveImageToFileToolStripMenuItem.Name = "saveImageToFileToolStripMenuItem";
            this.saveImageToFileToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.saveImageToFileToolStripMenuItem.Text = "Save Image to File...";
            this.saveImageToFileToolStripMenuItem.Click += new System.EventHandler(this.OnSaveImageToFileClick);
            // 
            // ImageGenerationSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PictureBoxPanel);
            this.Name = "ImageGenerationSummary";
            this.Size = new System.Drawing.Size(507, 415);
            this.PictureBoxPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DiagramViewerPictureBox)).EndInit();
            this.PictureBoxContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PictureBoxPanel;
        private System.Windows.Forms.PictureBox DiagramViewerPictureBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton ZoomSizeMode;
        private System.Windows.Forms.RadioButton NormalSizeModel;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton AutoSizeSizeMode;
        private System.Windows.Forms.RadioButton StretchImageSizeMode;
        private System.Windows.Forms.ContextMenuStrip PictureBoxContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem saveImageToFileToolStripMenuItem;
    }
}
