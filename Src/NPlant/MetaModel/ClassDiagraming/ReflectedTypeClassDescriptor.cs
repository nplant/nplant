using System;

namespace NPlant.MetaModel.ClassDiagraming
{
    public class ReflectedTypeClassDescriptor : AbstractClassDescriptor
    {
        public ReflectedTypeClassDescriptor(ClassDiagram diagram, Type type) : base(diagram, type)
        {
            this.Level = 1;
        }

        public override bool GetMemberVisibility(string name)
        {
            return true;
        }

        internal void SetLevel(int level)
        {
            this.Level = level;
        }
    }
}