using System;

namespace NPlant.MetaModel.ClassDiagraming
{
    public class ReflectedEnumDescriptor : ReflectedClassDescriptor
    {
        public ReflectedEnumDescriptor(Type enumType) : base(enumType)
        {
            this.RenderInheritance = false;
        }

        public override IDescriptorWriter GetWriter(ClassDiagram diagram)
        {
            return new EnumWriter(this.ReflectedType);
        }

        protected override void LoadMembers(Generation.ClassDiagraming.ClassDiagramVisitorContext context)
        {
            // don't load members if it's an enum
        }
    }
}