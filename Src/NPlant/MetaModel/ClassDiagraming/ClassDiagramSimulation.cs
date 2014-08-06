using NPlant.Core;
using NPlant.Generation.ClassDiagraming;

namespace NPlant.MetaModel.ClassDiagraming
{
    public class ClassDiagramSimulation
    {
        private readonly ClassDiagram _definition;
        private readonly KeyedList<ClassDescriptor> _classes = new KeyedList<ClassDescriptor>();

        public ClassDiagramSimulation(ClassDiagram diagram)
        {
            _definition = diagram;
        }

        public void Simulate()
        {
            using (new ClassDiagramGeneration(_definition))
            {
                // initialize all of the classes there were explicitly added via that diagram API
                foreach (var rootClass in _definition.RootClasses.InnerList)
                {
                    rootClass.Visit(ClassDiagramVisitorContext.Current);
                    _classes.Add(rootClass);
                }

                // the above initialization might have added more classes, so now visit all of those
                ClassDiagramVisitorContext.Current.VisitAllRelatedClasses();

                _classes.AddRange(ClassDiagramVisitorContext.Current.VisitedRelatedClasses);
            }
        }

        public KeyedList<ClassDescriptor> Classes
        {
            get { return _classes; }
        }

        public TypeMetaModelSet Types
        {
            get { return _definition.Types; }
        }
    }
}
