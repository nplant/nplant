using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NPlant.UI.Screens.Controls
{
    public partial class ImageGenerationSummary : UserControl
    {
        public ImageGenerationSummary()
        {
            InitializeComponent();
        }

        public Image Image
        {
            get { return this.DiagramViewerPictureBox.Image; }
            set { this.DiagramViewerPictureBox.Image = value; }
        }

        private void SizeModeChanged(object sender, EventArgs e)
        {
            RadioButton radio = (RadioButton) sender;

            PictureBoxSizeMode mode;

            if (Enum.TryParse(radio.Text, out mode))
            {
                DiagramViewerPictureBox.SizeMode = mode;
            }
        }

        private void OnPictureBoxContextMenuStripOpening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = this.Image == null;
        }

        private void OnSaveImageToFileClick(object sender, EventArgs e)
        {
            var result = ScreenManager.Launch<FileSaveScreen, FileDialogResult>(this);

            if (result.UserApproved)
            {
                this.Image.Save(result.FilePath);
                Process.Start(result.FilePath);
            }
        }
    }
}
