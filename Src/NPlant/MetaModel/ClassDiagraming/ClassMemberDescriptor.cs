using System;
using System.Reflection;
using NPlant.Core;

namespace NPlant.MetaModel.ClassDiagraming
{
    public class ClassMemberDescriptor : IKeyedItem
    {
        private readonly TypeMetaModel _metaModel;

        public ClassMemberDescriptor(string name, Type type, TypeMetaModel metaModel)
        {
            this.Name = name;
            this.MemberType = type;
            this.Key = this.Name;
            _metaModel = metaModel;
        }

        public string Name { get; private set; }
        public Type MemberType { get; private set; }
        public string Key { get; private set; }

        public TypeMetaModel MetaModel { get { return _metaModel; } }
    }
}
