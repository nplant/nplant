using NPlant.MetaModel.ClassDiagraming;

namespace NPlant.Generation.ClassDiagraming
{
    public abstract class ClassDiagramGenerator : IDiagramGenerator
    {
        protected readonly ClassDiagram Definition;

        protected ClassDiagramGenerator(ClassDiagram definition)
        {
            Definition = definition.CheckForNullArg("definition");
        }

        public void Generate()
        {
            using (new ClassDiagramGeneration(Definition))
            {
                // initialize all of the classes there were explicitly added via that diagram API
                foreach (var rootClass in Definition.RootClasses.InnerList)
                {
                    rootClass.Visit();
                    OnRootClassVisited(rootClass);
                }

                // the above initialization might have added more classes, so now visit all of those
                ClassDiagramVisitorContext.Current.VisitAllRelatedClasses();

                Finalize(ClassDiagramVisitorContext.Current);
            }
        }

        protected virtual void OnRootClassVisited(ClassDescriptor rootClass)
        {
        }

        protected abstract void Finalize(ClassDiagramVisitorContext current);
    }
}