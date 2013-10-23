using System;
using System.Drawing;

namespace NPlant.UI.Screens.FileViews
{
    public partial class ClassDiagramView : FileView, IClassDiagramView
    {
        private readonly ClassDiagramViewController _controller;

        public ClassDiagramView(string filePath)
        {
            InitializeComponent();

            _controller = new ClassDiagramViewController(this, filePath);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _controller.Start();
        }

        public string DiagramText
        {
            get { return this.DiagramTextTextBox.Text; } 
            set
            {
                this.DiagramTextTextBox.Text = value;
                this.Image = null;
            }
        }

        public bool ShowDiagramClassesPanel
        {
            get
            {
                return this.DiagramsComboBox.Visible;
            }
            set
            {
                this.DiagramsComboBox.Visible = value;
                this.DiagramClassesLabel.Visible = value;
            }
        }

        public LoadedDiagram[] Diagrams
        {
            get { return (LoadedDiagram[])this.DiagramsComboBox.Tag; }
            set
            {
                int? selectedIndex = null;

                if (value != null)
                {
                    for (int index = 0; index < value.Length; index++)
                    {
                        var diagram = value[index];
                        if (!selectedIndex.HasValue)
                            selectedIndex = index;

                        this.DiagramsComboBox.Items.Add(diagram);
                    }
                }

                if(selectedIndex.HasValue)
                    this.DiagramsComboBox.SelectedIndex = selectedIndex.Value;
                
                this.DiagramsComboBox.Tag = value;
            }
        }

        public LoadedDiagram SelectedDiagram
        {
            get { return this.DiagramsComboBox.SelectedItem as LoadedDiagram; }
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

        public Image Image
        {
            get { return this.ImageGenerationSummaryControl.Image; }
            set { this.ImageGenerationSummaryControl.Image = value; }
        }

        private void OnSelectedDiagramChanged(object sender, EventArgs e)
        {
            using (new WaitCursor())
                _controller.LoadDiagram((LoadedDiagram)this.DiagramsComboBox.SelectedItem);
        }
    }
}
