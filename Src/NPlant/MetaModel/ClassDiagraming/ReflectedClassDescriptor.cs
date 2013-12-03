using System;

namespace NPlant.MetaModel.ClassDiagraming
{
    public class ReflectedClassDescriptor : ClassDescriptor
    {
        public ReflectedClassDescriptor(Type type) : base(type)
        {
            this.Level = 1;
        }

        public override bool GetMemberVisibility(string name)
        {
            return true;
        }

        public override IDescriptorWriter GetWriter(ClassDiagram diagram)
        {
            return new ClassWriter(diagram, this);
        }

        internal void SetLevel(int level)
        {
            this.Level = level;
        }
    }
}