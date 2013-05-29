namespace NPlant.MetaModel.ClassDiagraming
{
    public class HasOneAggregationDescriptor : AbstractAggregationDescriptor
    {
        public HasOneAggregationDescriptor(IClassDiagramMetaModel diagramMetaModel, IClassDiagramClassDescriptor parent, ClassMemberDescriptor member)
            : base(diagramMetaModel, parent, member)
        {
        }

        public override string Verb
        {
            get { return "has a"; }
        }

        public override string Noun
        {
            get { return Member.Key; }
        }
    }
}