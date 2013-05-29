using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NPlant.Core;

namespace NPlant.MetaModel.ClassDiagraming
{
    public abstract class AbstractClassDescriptor : IClassDiagramClassDescriptor
    {
        private readonly KeyedList<IAggregationDescriptor> _associations = new KeyedList<IAggregationDescriptor>();
        private readonly ClassDiagram _diagram;
        protected internal readonly IDictionary<string, bool> MemberVisibility = new Dictionary<string, bool>();
        private readonly KeyedList<ClassMemberDescriptor> _members = new KeyedList<ClassMemberDescriptor>();

        protected AbstractClassDescriptor(ClassDiagram classDiagram, Type reflectedType)
        {
            _diagram = classDiagram;
            this.ReflectedType = reflectedType;
            this.Name = this.ReflectedType.Name;
            this.MetaModel = _diagram.MetaModel.Types[this.ReflectedType];
            _members.AddRange(GetClassMembers(this.ReflectedType));
        }

        private IEnumerable<ClassMemberDescriptor> GetClassMembers(Type type)
        {
            var descriptors = type.GetFields().Select(field => new ClassMemberDescriptor(field.Name, field.FieldType, _diagram.MetaModel.Types[field.FieldType])).ToList();

            descriptors.AddRange(type.GetProperties().Select(property => new ClassMemberDescriptor(property.Name, property.PropertyType, _diagram.MetaModel.Types[property.PropertyType])));

            return descriptors;
        }

        string IKeyedItem.Key { get { return this.Name; } }

        public string Name { get; protected set; }

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

        public KeyedList<IAggregationDescriptor> Associations { get { return _associations; } }
        
        public void AddAssociation(IAggregationDescriptor descriptor)
        {
            _associations.Add(descriptor);
        }

        public TypeMetaModel MetaModel { get; private set; }

        public IAggregationDescriptor IsAssociatedTo(ClassMemberDescriptor member)
        {
            if (this.GetMemberVisibility(member.Name))
            {
                IAggregationDescriptor descriptor;

                if (typeof(IEnumerable).IsAssignableFrom(member.MemberType))
                {
                    descriptor = new HasManyAggregationDescriptor(_diagram, this, member);
                }
                else
                {
                    descriptor = new HasOneAggregationDescriptor(_diagram, this, member);
                }

                _associations.Add(descriptor);

                _diagram.AddClass(new ReflectedTypeClassDescriptor(_diagram, member.MemberType));

                return descriptor;
            }

            return new NullIAggregationDescriptor();
        }

        public IEnumerable<IAggregationDescriptor> GetAssociations()
        {
            return _associations.InnerList;
        }
    }
}