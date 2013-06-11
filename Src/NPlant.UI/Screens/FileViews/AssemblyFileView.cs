using System.Drawing;
using NPlant.UI.Screens.FileViews;

namespace NPlant.UI
{
    public partial class AssemblyFileView : FileView, IAssemblyFileView
    {
        private readonly AssemblyFileViewController _controller;

        public AssemblyFileView(string filePath)
        {
            InitializeComponent();

            _controller = new AssemblyFileViewController(this, filePath);
        }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            _controller.Start();
        }

        public bool GenerateOnSelectionChanged
        {
            get { return GenerateOnSelectionChangedCheckBox.Checked; }
            set { GenerateOnSelectionChangedCheckBox.Checked = value; }
        }

        public LoadedDiagram[] Diagrams
        {
            get { return (LoadedDiagram[])this.DiagramsListBox.Tag; }
            set
            {
                if (value != null)
                {
                    foreach (var diagram in value)
                        this.DiagramsListBox.Items.Add(diagram);
                }

                this.DiagramsListBox.Tag = value;
            }
        }

        public LoadedDiagram SelectedDiagram 
        { 
            get { return this.DiagramsListBox.SelectedItem as LoadedDiagram; }
        }

        private void OnGenerateButtonClick(object sender, System.EventArgs e)
        {
            if (GenerateButton.Enabled)
            {
                using (new WaitCursor())
                {
                    this.ImageGenerationSummaryControl.Image = null;
                    _controller.Generate();
                }
            }
        }

        private void OnDiagramSelectionChanged(object sender, System.EventArgs e)
        {
            GenerateButton.Enabled = DiagramsListBox.SelectedItem != null;

            this.ImageGenerationSummaryControl.Image = null;

            if (this.GenerateOnSelectionChanged)
                OnGenerateButtonClick(sender, e);
        }

        public Image Image 
        { 
            get { return this.ImageGenerationSummaryControl.Image; } 
            set { this.ImageGenerationSummaryControl.Image = value; } 
        }
    }
}
