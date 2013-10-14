using System;
using NPlant.Core;

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

        internal void SetLevel(int level)
        {
            this.Level = level;
        }
    }
}