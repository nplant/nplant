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
        }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);
            _controller.Start();
        }

        public string DiagramText
        {
            get { return this.DiagramTextTextBox.Text; } 
            set { this.DiagramTextTextBox.Text = value; }
        }

        public void DiagramTextChanged(Action<string> action)
        {
            _onDiagramTextChanged = action;
        }

        private void OnDiagramTextChanged(object sender, EventArgs e)
        {
            _onDiagramTextChanged(this.DiagramText);
        }
    }
}
