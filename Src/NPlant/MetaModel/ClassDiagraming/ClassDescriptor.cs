using System;
using System.Linq.Expressions;
using System.Reflection;
using NPlant.Core;

namespace NPlant.MetaModel.ClassDiagraming
{
    public class ClassDescriptor<T> : AbstractClassDescriptor
    {
        public ClassDescriptor(ClassDiagram classDiagram) : base(classDiagram, typeof(T))
        {
        }

        public ClassDescriptor<T> Named(string name)
        {
            this.Name = name;
            return this;
        }


        public ClassDescriptor<T> ShowInheritors()
        {
            this.RenderInheritance = true;
            return this;
        }

        public ClassDescriptor<T> HideInheritors()
        {
            this.RenderInheritance = false;
            return this;
        }

        public ClassDescriptor<T> HideMember<TMember>(Expression<Func<T, TMember>> expression)
        {
            var member = ReflectOn<T>.ForMember(expression);
            MemberVisibility[member.Name] = false;

            return this;
        }

        public ForMemberDescriptor<T> ForMember<TMember>(Expression<Func<T, TMember>> expression)
        {
            return new ForMemberDescriptor<T>(this, ReflectOn<T>.ForMember(expression));
        }

        public class ForMemberDescriptor<TMember>
        {
            private readonly AbstractClassDescriptor _descriptor;
            private readonly MemberInfo _member;

            public ForMemberDescriptor(AbstractClassDescriptor descriptor, MemberInfo member)
            {
                _descriptor = descriptor;
                _member = member;
            }

            public ForMemberDescriptor<TMember> Hide()
            {
                _descriptor.MemberVisibility[_member.Name] = false;
                return this;
            }

            public ForMemberDescriptor<TMember> CustomerDiagram<TForMember>(Expression<Func<TMember, TForMember>> expression)
            {
                return new ForMemberDescriptor<TMember>(_descriptor, ReflectOn<TMember>.ForMember(expression));
            }
        }
    }
}
