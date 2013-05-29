namespace NPlant.MetaModel.ClassDiagraming
{
    public class HasManyAggregationDescriptor : AbstractAggregationDescriptor
    {
        public HasManyAggregationDescriptor(IClassDiagramMetaModel diagramMetaModel, IClassDiagramClassDescriptor parent, ClassMemberDescriptor member)
            : base(diagramMetaModel, parent, member)
        {
        }

        public override string Verb
        {
            get { return "has many"; }
        }

        public override string Noun
        {
            get { return Member.Key; }
        }
    }
}