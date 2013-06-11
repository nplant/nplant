using System;
using System.Drawing;

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
            get { return 0; }
            set { }
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
            {
                this.ImageGenerationSummaryControl.Image = null;
                _controller.Generate();
            }
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

        public Image Image
        {
            get { return this.ImageGenerationSummaryControl.Image; }
            set { this.ImageGenerationSummaryControl.Image = value; }
        }
    }
}
