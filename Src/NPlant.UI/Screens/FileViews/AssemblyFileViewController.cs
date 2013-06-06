using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NPlant.Core;
using NPlant.Generation;
using NPlant.UI.Screens.FileViews;

namespace NPlant.UI
{
    public class AssemblyFileViewController : FileViewController
    {
        private readonly IAssemblyFileView _view;
        private readonly string _filePath;

        public AssemblyFileViewController(IAssemblyFileView view, string filePath) : base(view)
        {
            _view = view;
            _filePath = filePath;
        }

        public void Start()
        {
            var assemblyLoader = new NPlantAssemblyLoader();
            Assembly assembly = assemblyLoader.Load(_filePath);

            var diagramLoader = new NPlantDiagramLoader();

            IEnumerable<IDiagram> diagrams = diagramLoader.Load(assembly);
            _view.Diagrams = diagrams.Select(diagram => new LoadedDiagram(diagram)).ToArray();
        }

        protected override string GetFilePath()
        {
            LoadedDiagram diagram = _view.SelectedDiagram;

            if(diagram != null)
                return ClassDiagramFile.Save(SystemEnvironment.ExecutionDirectory, diagram.Diagram, NullRecorder.Instance);

            return null;
        }
    }
}