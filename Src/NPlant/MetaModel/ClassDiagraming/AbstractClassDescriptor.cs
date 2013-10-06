using System;
using System.Collections.Generic;
using System.Linq;
using NPlant.Core;

namespace NPlant.MetaModel.ClassDiagraming
{
    public abstract class AbstractClassDescriptor : IClassDiagramClassDescriptor
    {
        private readonly KeyedList<IRelationDescriptor> _relations = new KeyedList<IRelationDescriptor>();
        private readonly ClassDiagram _diagram;
        protected internal readonly IDictionary<string, bool> MemberVisibility = new Dictionary<string, bool>();
        private readonly KeyedList<ClassMemberDescriptor> _members = new KeyedList<ClassMemberDescriptor>();

        protected AbstractClassDescriptor(ClassDiagram classDiagram, Type reflectedType)
        {
            this.RenderInheritance = true;
            _diagram = classDiagram;
            this.ReflectedType = reflectedType;
            this.Name = this.ReflectedType.Name;
            this.MetaModel = _diagram.MetaModel.Types[this.ReflectedType];
            _members.AddRange(GetClassMembers(this.ReflectedType));
        }

        private IEnumerable<ClassMemberDescriptor> GetClassMembers(Type type)
        {
            var descriptors = type.GetFields().Select(field => new ClassMemberDescriptor(type, field, _diagram.MetaModel.Types[field.FieldType])).ToList();

            descriptors.AddRange(type.GetProperties().Select(property => new ClassMemberDescriptor(type, property, _diagram.MetaModel.Types[property.PropertyType])));

            return descriptors;
        }

        string IKeyedItem.Key { get { return this.Name; } }

        public string Name { get; protected set; }

        public bool RenderInheritance { get; set; }

        protected internal ClassDiagram DiagramDefinition
        {
            get { return _diagram; }
        }

        public Type ReflectedType { get; private set; }

        public int Level { get; protected set; }
        
        public KeyedList<ClassMemberDescriptor> Members { get { return _members; }}

        public virtual bool GetMemberVisibility(string name)
        {
            bool visibility;

            if (MemberVisibility.TryGetValue(name, out visibility))
                return visibility;

            // default to visible (i.e. if no specification is present, assume visible)
            return true;
        }

        public KeyedList<IRelationDescriptor> Relations { get { return _relations; } }
        
        public void AddRelation(IRelationDescriptor descriptor)
        {
            _relations.Add(descriptor);
        }

        public TypeMetaModel MetaModel { get; private set; }

        public IEnumerable<IRelationDescriptor> GetAssociations()
        {
            return _relations.InnerList;
        }
    }
}