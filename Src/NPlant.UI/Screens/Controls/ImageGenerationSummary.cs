using System;
using System.Windows.Forms;

namespace NPlant.UI.Screens.Controls
{
    public partial class ImageGenerationSummary : UserControl
    {
        private ImageGenerationSummaryDisplayMode _mode;

        private delegate void LoadImageCallback();

        public ImageGenerationSummary()
        {
            InitializeComponent();

            this.Mode = ImageGenerationSummaryDisplayMode.ProgressBar;
        }

        private void LoadImage()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new LoadImageCallback(LoadImage), new object[]{});
            }
            else
            {
                SetProgress(100);
                this.DiagramViewerPictureBox.ImageLocation = this.FilePath;
            }
        }

        public string FilePath
        {
            get { return this.LoadedFilePathTextBox.Text; }
            set
            {
                this.LoadedFilePathTextBox.Text = value;

                if (!value.IsNullOrEmpty())
                {
                    var watcher = new ImageFileWatcher(value, LoadImage);
                    watcher.Watch();
                }
            }
        }

        public ImageGenerationSummaryDisplayMode Mode
        {
            get { return _mode; }
            set
            {
                _mode = value;

                this.GenerationProgressBar.Visible = this.IsProgressBarMode;
                this.LoadedFilePathTextBox.Visible = ! this.GenerationProgressBar.Visible;
            }
        }

        public int GetProgress()
        {
            return IsProgressBarMode ? this.GenerationProgressBar.Value : 0;
        }

        public void SetProgress(int value)
        {
            if (!this.IsProgressBarMode)
                this.Mode = ImageGenerationSummaryDisplayMode.ProgressBar;

            if (value < 1)
                value = 0;
            if (value > 100)
                value = 100;
                
            this.GenerationProgressBar.Value = value;

            if(this.GenerationProgressBar.Value == 100)
                this.Mode = ImageGenerationSummaryDisplayMode.FilePath;
        }

        private bool IsProgressBarMode
        {
            get { return this.Mode == ImageGenerationSummaryDisplayMode.ProgressBar; }
        }

        public enum ImageGenerationSummaryDisplayMode
        {
            ProgressBar,
            FilePath
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
    }
}
