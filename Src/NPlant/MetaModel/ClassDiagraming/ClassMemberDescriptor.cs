using System;
using System.Reflection;
using NPlant.Core;

namespace NPlant.MetaModel.ClassDiagraming
{
    public class ClassMemberDescriptor : IKeyedItem
    {
        private readonly TypeMetaModel _metaModel;

        public ClassMemberDescriptor(Type containingType, PropertyInfo member, TypeMetaModel metaModel)
        {
            this.Name = member.Name;
            this.MemberType = member.PropertyType;
            this.Key = this.Name;
            _metaModel = metaModel;
            this.IsInherited = member.DeclaringType != containingType;
        }

        public ClassMemberDescriptor(Type containingType, FieldInfo member, TypeMetaModel metaModel)
        {
            this.Name = member.Name;
            this.MemberType = member.FieldType;
            this.Key = this.Name;
            _metaModel = metaModel;
            this.IsInherited = member.DeclaringType != containingType;
        }

        public bool IsInherited { get; private set; }
        public string Name { get; private set; }
        public Type MemberType { get; private set; }
        public string Key { get; private set; }

        public TypeMetaModel MetaModel { get { return _metaModel; } }

        internal bool TreatAsPrimitive { get; set; }
    }
}
