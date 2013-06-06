using System;

namespace NPlant.UI.Screens.FileViews
{
    public partial class NPlantFileView : FileView, INPlantFileView
    {
        private readonly NPlantFileViewController _controller;
        private Action<string> _onDiagramTextChanged = text => {};

        public NPlantFileView(string filePath)
        {
            InitializeComponent();

            _controller = new NPlantFileViewController(this, filePath);
            this.ImageGenerationSummaryControl.FilePath = filePath.Replace(".nplant", ".png");
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _controller.Start();
        }

        public string DiagramText
        {
            get { return this.DiagramTextTextBox.Text; } 
            set { this.DiagramTextTextBox.Text = value; }
        }

        public int Progress
        {
            get { return this.ImageGenerationSummaryControl.GetProgress(); }
            set { this.ImageGenerationSummaryControl.SetProgress(value); }
        }

        public void DiagramTextChanged(Action<string> action)
        {
            _onDiagramTextChanged = action;
        }

        private void OnDiagramTextChanged(object sender, EventArgs e)
        {
            _onDiagramTextChanged(this.DiagramText);
        }

        private void OnSaveButtonClick(object sender, EventArgs e)
        {
            using (new WaitCursor())
                _controller.Save();
        }

        private void OnCopyButtonClick(object sender, EventArgs e)
        {
            using (new WaitCursor())
                _controller.Copy();
        }

        private void OnGenerateButtonClick(object sender, EventArgs e)
        {
            using (new WaitCursor())
                _controller.Generate();
        }

        public bool GenerateOnTextChange
        {
            get { return this.GenerateOnTextChangeCheckBox.Checked; }
            set { this.GenerateOnTextChangeCheckBox.Checked = value; }
        }

        private void OnGenerateOnTextChangeCheckChanged(object sender, EventArgs e)
        {
            if(this.GenerateOnTextChange)
                this.DiagramTextTextBox.TextChanged += this.OnDiagramTextChanged;
            else
                this.DiagramTextTextBox.TextChanged -= this.OnDiagramTextChanged;
        }
    }
}
