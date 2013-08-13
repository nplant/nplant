using NPlant.MetaModel.ClassDiagraming;

namespace NPlant.Generation.ClassDiagraming
{
    public abstract class RelationBuilder : IBuilder
    {
        private readonly IRelationDescriptor _aggregation;

        protected RelationBuilder(IRelationDescriptor aggregation)
        {
            _aggregation = aggregation;
        }

        public void Build(ClassDiagramGenerationContext context)
        {
            var newLine = "\\n";

            if (string.IsNullOrEmpty(_aggregation.Noun))
                newLine = null;

            context.WriteLine("\"{0}\" {1} \"{2}\" : \"{3}{4}{5}\""
                .FormatWith(
                    _aggregation.LeftName, 
                    this.Symbol, 
                    _aggregation.RightName, 
                    _aggregation.Verb, 
                    newLine,
                    _aggregation.Noun
                )
            );
        }

        protected abstract string Symbol { get; }
    }
}