using System;
using NPlant.MetaModel.ClassDiagraming;

namespace NPlant.Generation.ClassDiagraming
{
    public class SimpleMemberBuilder : IBuilder
    {
        private readonly IClassDiagramClassDescriptor _classDescriptor;
        private readonly ClassMemberDescriptor _member;

        public SimpleMemberBuilder(ClassMemberDescriptor member, IClassDiagramClassDescriptor classDescriptor)
        {
            _member = member;
            _classDescriptor = classDescriptor;
        }

        void IBuilder.Build(ClassDiagramGenerationContext context)
        {
            bool visible = _classDescriptor.GetMemberVisibility(_member.Key);

            if (visible)
            {
                var typeMetaModel = context.MetaModel.Types[_member.MemberType];
                context.WriteLine(String.Format("    {0} {1}", typeMetaModel.Name, _member.Key));
            }
        }
    }
}