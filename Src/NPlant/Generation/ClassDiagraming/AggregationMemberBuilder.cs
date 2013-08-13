using NPlant.MetaModel.ClassDiagraming;

namespace NPlant.Generation.ClassDiagraming
{
    public class AggregationMemberBuilder : IBuilder
    {
        private IRelationDescriptor _descriptor;

        public AggregationMemberBuilder(IRelationDescriptor descriptor)
        {
            _descriptor = descriptor;
        }

        public void Build(ClassDiagramGenerationContext context)
        {

        }
    }
}