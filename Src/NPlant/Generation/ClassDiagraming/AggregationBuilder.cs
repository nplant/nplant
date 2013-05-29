using NPlant.MetaModel.ClassDiagraming;

namespace NPlant.Generation.ClassDiagraming
{
    public class AggregationBuilder : IBuilder
    {
        private readonly IAggregationDescriptor _aggregation;

        public AggregationBuilder(IAggregationDescriptor aggregation)
        {
            _aggregation = aggregation;
        }

        void IBuilder.Build(ClassDiagramGenerationContext context)
        {
            context.WriteLine("\"{0}\" --o \"{2}\" : \"{3}\\n{1}\"".FormatWith(_aggregation.LeftName, _aggregation.Noun, _aggregation.RightName, _aggregation.Verb));
        }
    }
}