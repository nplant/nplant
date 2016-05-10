using System;
using System.Reflection;
using NPlant.Core;
using NPlant.Generation.ClassDiagraming;

namespace NPlant.MetaModel.ClassDiagraming
{
    public class ClassMemberDescriptor : IKeyedItem
    {
        private readonly TypeMetaModel _metaModel;
        private readonly ClassDescriptor _descriptor;

        public ClassMemberDescriptor(ClassDescriptor descriptor, MemberInfo member)
        {
            var property = member as PropertyInfo;
            this.AccessModifier = AccessModifier.Public;

            if (property != null)
            {
                this.MemberType = property.PropertyType;
            }

            var field = member as FieldInfo;

            if (field != null)
            {
                this.MemberType = field.FieldType;
            }

            this.AccessModifier = AccessModifier.GetAccessModifier(member);


            if (this.MemberType == null)
                throw new NPlantException("Member's could not be interpretted as either a property or a field");
            
            _descriptor = descriptor;
            
            this.Name = member.Name;
            this.Key = this.Name;
            _metaModel = ClassDiagramVisitorContext.Current.GetTypeMetaModel(this.MemberType);
            this.IsInherited = member.DeclaringType != descriptor.ReflectedType;
        }

        public bool IsInherited { get; private set; }
        
        public string Name { get; private set; }
        
        public Type MemberType { get; private set; }

        public AccessModifier AccessModifier { get; private set; }
        
        public string Key { get; private set; }

        public bool IsHidden
        {
            get
            {
                if (this.MetaModel.Hidden)
                    return true;

                return !_descriptor.GetMemberVisibility(this.Name);
            }
        }

        public bool IsVisible
        {
            get { return !this.IsHidden; }
        }

        public TypeMetaModel MetaModel { get { return _metaModel; } }

        internal bool TreatAsPrimitive { get; set; }
    }
}
