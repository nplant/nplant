using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using NPlant.Core;
using NPlant.Generation;
using NPlant.Generation.ClassDiagraming;

namespace NPlant.UI.Screens.FileViews
{
    public class ClassDiagramViewController : FileViewController
    {
        private readonly IClassDiagramView _view;
        private readonly string _filePath;
        private string _fileToSave;

        public ClassDiagramViewController(IClassDiagramView view, string filePath) : base(view)
        {
            _view = view;
            _filePath = filePath;
        }

        public void Start()
        {
            var file = new FileInfo(_filePath);

            if (file.Exists)
            {
                if (file.IsNPlantFile())
                    LoadNPlantFile();
                else if (file.IsAssembly())
                    LoadAssembly();
            }
        }

        private void LoadAssembly()
        {
            var assemblyLoader = new NPlantAssemblyLoader();
            Assembly assembly = assemblyLoader.Load(_filePath);

            var diagramLoader = new NPlantDiagramLoader();

            var diagrams = diagramLoader.Load(assembly);
            _view.Diagrams = diagrams.Select(diagram => new LoadedDiagram(diagram.Diagram)).ToArray();

            _view.ShowDiagramClassesPanel = true;
            _fileToSave = Path.ChangeExtension(_filePath, ".nplant");
        }

        private void LoadNPlantFile()
        {
            _view.DiagramText = File.ReadAllText(_filePath);
            _view.ShowDiagramClassesPanel = false;
            _fileToSave = _filePath;
        }

        public void Save()
        {
            if (! File.Exists(_fileToSave))
            {
                PromptDialog prompt = new PromptDialog("Save to?", _fileToSave) {RequireValue = true};

                if (prompt.ShowDialog() == DialogResult.OK)
                {
                    _fileToSave = prompt.Value;

                    File.WriteAllText(_fileToSave, _view.DiagramText);
                }
            }
            else
            {
                File.WriteAllText(_fileToSave, _view.DiagramText);
            }
        }

        public void Copy()
        {
            Clipboard.SetText(_view.DiagramText);
        }

        protected override string GetDiagramText()
        {
            return _view.DiagramText;
        }

        protected override string GetDiagramName()
        {
            if (_view.SelectedDiagram == null)
                return null;

            return _view.SelectedDiagram.Diagram.Name;
        }

        public void LoadDiagram(LoadedDiagram diagram)
        {
            _view.DiagramText = BufferedClassDiagramGenerator.GetDiagramText(diagram.Diagram);
        }
    }
}