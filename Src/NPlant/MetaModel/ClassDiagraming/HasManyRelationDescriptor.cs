using NPlant.Core;
using NPlant.Generation.ClassDiagraming;

namespace NPlant.MetaModel.ClassDiagraming
{
    public class HasManyRelationDescriptor : AbstractRelationDescriptor
    {
        public HasManyRelationDescriptor(string key, IClassDiagramClassDescriptor source, TypeMetaModel target)
            : base(key, source, target)
        {
        }

        public override string Verb
        {
            get { return "has many"; }
        }

        public override Generation.IBuilder GetBuilder()
        {
            return new HasAggregationBuilder(this);
        }
    }
}