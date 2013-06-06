using System.Windows.Forms;

namespace NPlant.UI.Screens.Controls
{
    public partial class ImageGenerationSummary : UserControl
    {
        private ImageGenerationSummaryDisplayMode _mode;

        public ImageGenerationSummary()
        {
            InitializeComponent();

            this.Mode = ImageGenerationSummaryDisplayMode.ProgressBar;
        }

        public string FilePath
        {
            get { return this.LoadedFilePathTextBox.Text; }
            set { this.LoadedFilePathTextBox.Text = value; }
        }

        public ImageGenerationSummaryDisplayMode Mode
        {
            get { return _mode; }
            set
            {
                _mode = value;

                this.GenerationProgressBar.Visible = this.IsProgressBarMode;
                this.LoadedFilePathTextBox.Visible = ! this.GenerationProgressBar.Visible;

                if (this.LoadedFilePathTextBox.Visible)
                    this.DiagramViewerPictureBox.ImageLocation = this.FilePath;
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
    }
}
