using System;
using System.Collections.Generic;
using System.Linq;
using NPlant.Core;
using NPlant.Generation.ClassDiagraming;

namespace NPlant.MetaModel.ClassDiagraming
{
    public abstract class AbstractClassDescriptor  : IKeyedItem
    {
        protected internal readonly IDictionary<string, bool> MemberVisibility = new Dictionary<string, bool>();
        private readonly KeyedList<ClassMemberDescriptor> _members = new KeyedList<ClassMemberDescriptor>();

        protected AbstractClassDescriptor(Type reflectedType)
        {
            this.RenderInheritance = true;
            this.ReflectedType = reflectedType;
            this.Name = this.ReflectedType.Name;
        }

        public void Visit(ClassDiagramVisitorContext context)
        {
            this.MetaModel = context.GetTypeMetaModel(this.ReflectedType);
            var descriptors = this.ReflectedType.GetFields().Select(field => new ClassMemberDescriptor(this.ReflectedType, field, context.GetTypeMetaModel(field.FieldType))).ToList();

            descriptors.AddRange(this.ReflectedType.GetProperties().Select(property => new ClassMemberDescriptor(this.ReflectedType, property, context.GetTypeMetaModel(property.PropertyType))));

            _members.AddRange(descriptors);

            bool showInheritance = this.RenderInheritance && this.ReflectedType.BaseType != null;
            TypeMetaModel baseTypeMetaModel = null;

            if (showInheritance)
            {
                baseTypeMetaModel = context.GetTypeMetaModel(this.ReflectedType.BaseType);

                showInheritance = !baseTypeMetaModel.HideAsBaseClass && !baseTypeMetaModel.Hidden;
            }

            if (!this.MetaModel.Hidden)
            {
                foreach (ClassMemberDescriptor member in this.Members.InnerList)
                {
                    TypeMetaModel metaModel = member.MetaModel;

                    if (!metaModel.Hidden)
                    {
                        // if not showing inheritance then show all members
                        // otherwise, only show member that aren't inherited
                        if (!showInheritance || !member.IsInherited)
                        {
                            if (metaModel.IsComplexType && this.GetMemberVisibility(member.Key))
                            {
                                var nextLevel = this.Level + 1;

                                if (member.MemberType.IsEnumerable())
                                {
                                    var enumeratorType = member.MemberType.GetEnumeratorType();
                                    var enumeratorTypeMetaModel = context.GetTypeMetaModel(enumeratorType);

                                    if (enumeratorTypeMetaModel.IsComplexType)
                                    {
                                        context.AddRelatedClass(this, new ReflectedClassDescriptor(enumeratorType), ClassDiagramRelationshipTypes.HasMany, nextLevel, member.Name);
                                    }
                                }
                                else
                                {
                                    context.AddRelatedClass(this, new ReflectedClassDescriptor(member.MemberType), ClassDiagramRelationshipTypes.HasA, nextLevel, member.Name);
                                }
                            }
                        }
                    }
                }
            }

            if (showInheritance)
            {
                context.AddRelatedClass(this, new ReflectedClassDescriptor(this.ReflectedType.BaseType), ClassDiagramRelationshipTypes.Base, this.Level - 1);
            }
        }

        string IKeyedItem.Key { get { return this.Name; } }

        public string Name { get; protected set; }

        public bool RenderInheritance { get; set; }

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

        public TypeMetaModel MetaModel { get; private set; }

        public string Color { get; private set; }

        public override int GetHashCode()
        {
            return this.ReflectedType.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            AbstractClassDescriptor descriptor = obj as AbstractClassDescriptor;

            if (descriptor == null)
                return false;

            return descriptor.ReflectedType == this.ReflectedType;
        }
    }
}