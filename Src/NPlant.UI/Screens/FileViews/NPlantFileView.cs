using System.Windows.Forms;

namespace NPlant.UI.Screens.FileViews
{
    public partial class NPlantFileView : FileView, INPlantFileView
    {
        private readonly NPlantFileViewController _controller;

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
    }
}
