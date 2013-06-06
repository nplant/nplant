using System.IO;
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
            using (new WaitCursor())
                _controller.Generate();
        }

        private void OnDiagramSelectionChanged(object sender, System.EventArgs e)
        {
            if (this.GenerateOnSelectionChanged)
                OnGenerateButtonClick(sender, e);

            this.ImageGenerationSummaryControl.FilePath = Path.Combine(SystemEnvironment.ExecutionDirectory, "{0}.png".FormatWith(DiagramsListBox.SelectedItem));
        }

        public int Progress
        {
            get { return this.ImageGenerationSummaryControl.GetProgress(); } 
            set { this.ImageGenerationSummaryControl.SetProgress(value); }
        }
    }
}
