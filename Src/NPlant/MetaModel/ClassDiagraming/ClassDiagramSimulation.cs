using NPlant.Core;
using NPlant.Generation.ClassDiagraming;

namespace NPlant.MetaModel.ClassDiagraming
{
    public class ClassDiagramSimulation
    {
        private readonly ClassDiagram _definition;
        private readonly ClassDiagramVisitorContext _context;
        private readonly KeyedList<AbstractClassDescriptor> _classes = new KeyedList<AbstractClassDescriptor>();

        public ClassDiagramSimulation(ClassDiagram diagram)
        {
            _definition = diagram;
            _context = _definition.CreateGenerationContext();
        }

        public void Simulate()
        {
            // initialize all of the classes there were explicitly added via that diagram API
            foreach (var rootClass in _definition.RootClasses.InnerList)
            {
                rootClass.Visit(_context);
                _classes.Add(rootClass);
            }

            // the above initialization might have added more classes, so now visit all of those
            _context.VisitAllRelatedClasses();

            _classes.AddRange(_context.VisitedRelatedClasses);
        }

        public KeyedList<AbstractClassDescriptor> Classes
        {
            get { return _classes; }
        }

        public TypeMetaModelSet Types
        {
            get { return _definition.Types; }
        }
    }
}
