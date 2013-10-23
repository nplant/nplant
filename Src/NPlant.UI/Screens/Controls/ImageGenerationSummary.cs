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
            set
            {
                MutatePictureBox(pic => pic.SetImage(value));
            }
        }

        private void MutatePictureBox(Action<SmartPictureBox> action)
        {
            action(this.DiagramViewerPictureBox);
            this.ActiveControl = this.DiagramViewerPictureBox;
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
                var path = this.Image.SaveNPlantImage(result.FilePath);
                Process.Start(path);
            }
        }

        private void OnPictureBoxClick(object sender, EventArgs e)
        {
            this.ActiveControl = this.DiagramViewerPictureBox;
        }
    }
}
