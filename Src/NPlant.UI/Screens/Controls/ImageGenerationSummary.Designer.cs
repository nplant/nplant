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
            this.PictureBoxContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveImageToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PictureBoxContainer = new System.Windows.Forms.Panel();
            this.DiagramViewerPictureBox = new NPlant.UI.Screens.Controls.SmartPictureBox();
            this.PictureBoxContextMenuStrip.SuspendLayout();
            this.PictureBoxContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DiagramViewerPictureBox)).BeginInit();
            this.SuspendLayout();
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
            // PictureBoxContainer
            // 
            this.PictureBoxContainer.AutoScroll = true;
            this.PictureBoxContainer.AutoSize = true;
            this.PictureBoxContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PictureBoxContainer.Controls.Add(this.DiagramViewerPictureBox);
            this.PictureBoxContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureBoxContainer.Location = new System.Drawing.Point(0, 0);
            this.PictureBoxContainer.Name = "PictureBoxContainer";
            this.PictureBoxContainer.Size = new System.Drawing.Size(609, 519);
            this.PictureBoxContainer.TabIndex = 7;
            // 
            // DiagramViewerPictureBox
            // 
            this.DiagramViewerPictureBox.BackColor = System.Drawing.Color.White;
            this.DiagramViewerPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DiagramViewerPictureBox.ContextMenuStrip = this.PictureBoxContextMenuStrip;
            this.DiagramViewerPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DiagramViewerPictureBox.Location = new System.Drawing.Point(0, 0);
            this.DiagramViewerPictureBox.Name = "DiagramViewerPictureBox";
            this.DiagramViewerPictureBox.Size = new System.Drawing.Size(609, 519);
            this.DiagramViewerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.DiagramViewerPictureBox.TabIndex = 7;
            this.DiagramViewerPictureBox.TabStop = false;
            // 
            // ImageGenerationSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PictureBoxContainer);
            this.Name = "ImageGenerationSummary";
            this.Size = new System.Drawing.Size(609, 519);
            this.PictureBoxContextMenuStrip.ResumeLayout(false);
            this.PictureBoxContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DiagramViewerPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip PictureBoxContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem saveImageToFileToolStripMenuItem;
        private System.Windows.Forms.Panel PictureBoxContainer;
        private SmartPictureBox DiagramViewerPictureBox;
    }
}
