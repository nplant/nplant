using NPlant.MetaModel.ClassDiagraming;

namespace NPlant.Generation.ClassDiagraming
{
    internal class ClassDiagramGenerator : IDiagramGenerator
    {
        private readonly ClassDiagram _definition;
        private readonly IClassDiagramMetaModel _metaModel;

        internal ClassDiagramGenerator(ClassDiagram definition)
        {
            _definition = definition.CheckForNullArg("definition");
            _metaModel = definition.MetaModel;
        }

        string IDiagramGenerator.Generate()
        {
            var context = new ClassDiagramGenerationContext(_definition);

            int counter = 0;
            IClassDiagramClassDescriptor classDescriptor;

            while (_metaModel.Classes.TryGetValueByIndex(counter, out classDescriptor))
            {
                if (!_definition.DepthLimit.HasValue || _definition.DepthLimit >= classDescriptor.Level)
                {
                    IBuilder builder = new ClassBuilder(classDescriptor);
                    builder.Build(context);                    
                }

                counter++;
            }

            return context.Complete();
        }
    }
}