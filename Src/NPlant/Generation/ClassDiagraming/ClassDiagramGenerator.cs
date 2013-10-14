using NPlant.MetaModel.ClassDiagraming;

namespace NPlant.Generation.ClassDiagraming
{
    public class ClassDiagramGenerator : IDiagramGenerator
    {
        private readonly ClassDiagram _definition;

        public ClassDiagramGenerator(ClassDiagram definition)
        {
            _definition = definition.CheckForNullArg("definition");
        }

        public string Generate()
        {
            ClassDiagramVisitorContext context = _definition.CreateGenerationContext();

            // initialize all of the classes there were explicitly added via that diagram API
            foreach (var rootClass in _definition.RootClasses.InnerList)
            {
                rootClass.Visit(context);
            }

            // the above initialization might have added more classes, so now visit all of those
            context.VisitAllRelatedClasses();

            // invoke the formatter and format the diagram
            var formatter = _definition.CreateFormatter(context);
            return formatter.Format();
        }
    }
}