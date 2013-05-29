using NPlant.Core;

namespace NPlant.MetaModel.ClassDiagraming
{
    public class ClassDiagramSimulation : IClassDiagramMetaModel
    {
        private readonly ClassDiagram _diagram;

        public ClassDiagramSimulation(ClassDiagram diagram)
        {
            _diagram = diagram;
        }

        public void Simulate()
        {
            var generator = _diagram.CreateGenerator();
            generator.Generate();
        }

        public KeyedList<IClassDiagramClassDescriptor> Classes
        {
            get { return _diagram.MetaModel.Classes; }
        }

        public TypeMetaModelSet Types
        {
            get { return _diagram.MetaModel.Types; }
        }
    }
}
