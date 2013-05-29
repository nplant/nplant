namespace NPlant.MetaModel.ClassDiagraming
{
    public abstract class AbstractAggregationDescriptor : IAggregationDescriptor
    {
        protected readonly ClassMemberDescriptor Member;
        protected readonly IClassDiagramClassDescriptor Parent;
        protected readonly IClassDiagramMetaModel DiagramMetaModel;

        protected AbstractAggregationDescriptor(IClassDiagramMetaModel diagramMetaModel, IClassDiagramClassDescriptor parent, ClassMemberDescriptor member)
        {
            DiagramMetaModel = diagramMetaModel;
            Parent = parent;
            Member = member;
        }

        public string Key { get { return Member.Key; } }
        
        public string LeftName { get { return Parent.Name; } }
        
        public string RightName
        {
            get
            {
                return DiagramMetaModel.Types[Member.MemberType].Name;
            }
        }

        public abstract string Verb { get; }
        
        public abstract string Noun { get; }
    }
}