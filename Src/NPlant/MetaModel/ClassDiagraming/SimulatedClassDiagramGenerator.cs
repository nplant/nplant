using NPlant.Core;
using NPlant.Generation.ClassDiagraming;

namespace NPlant.MetaModel.ClassDiagraming
{
    public class SimulatedClassDiagramGenerator : ClassDiagramGenerator
    {
        private readonly ClassDiagram _definition;
        private readonly KeyedList<ClassDescriptor> _classes = new KeyedList<ClassDescriptor>();

        public SimulatedClassDiagramGenerator(ClassDiagram diagram) : base(diagram)
        {
            _definition = diagram;
        }

        protected override void OnRootClassVisited(ClassDescriptor rootClass)
        {
            _classes.Add(rootClass);
        }

        public KeyedList<ClassDescriptor> Classes
        {
            get { return _classes; }
        }

        public TypeMetaModelSet Types
        {
            get { return _definition.Types; }
        }

        protected override void Finalize(ClassDiagramVisitorContext current)
        {
            _classes.AddRange(current.VisitedRelatedClasses);
        }
    }
}
