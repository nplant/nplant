using NPlant.MetaModel.ClassDiagraming;

namespace NPlant.Generation.ClassDiagraming
{
    public class ExtensionBuilder : RelationBuilder
    {
        public ExtensionBuilder(IRelationDescriptor aggregation) : base(aggregation)
        {
        }

        protected override string Symbol
        {
            get { return "-up-|>"; }
        }
    }
}