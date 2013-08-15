using NPlant.MetaModel.ClassDiagraming;

namespace NPlant.Generation.ClassDiagraming
{
    public class HasAggregationBuilder : RelationBuilder
    {
        public HasAggregationBuilder(IRelationDescriptor aggregation) : base(aggregation)
        {
        }

        protected override string Symbol
        {
            get { return "o--"; }
        }
    }
}