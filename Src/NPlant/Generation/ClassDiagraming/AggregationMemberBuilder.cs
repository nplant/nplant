using NPlant.MetaModel.ClassDiagraming;

namespace NPlant.Generation.ClassDiagraming
{
    public class AggregationMemberBuilder : IBuilder
    {
        private IAggregationDescriptor _descriptor;

        public AggregationMemberBuilder(IAggregationDescriptor descriptor)
        {
            _descriptor = descriptor;
        }

        void IBuilder.Build(ClassDiagramGenerationContext context)
        {

        }
    }
}