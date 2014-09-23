using System.Reflection;

namespace NPlant.MetaModel.ClassDiagraming
{
    public struct AccessModifier
    {
        private AccessModifier(string name, string notation) : this()
        {
            Name = name;
            Notation = notation;
        }

        public string Name { get; private set; }

        public string Notation { get; private set; }

        public static readonly AccessModifier Unknown = new AccessModifier("Public", "+");
        public static readonly AccessModifier Public = new AccessModifier("Public", "+");
        public static readonly AccessModifier Private = new AccessModifier("Private", "-");
        public static readonly AccessModifier Protected = new AccessModifier("Protected", "#");
        public static readonly AccessModifier Internal = new AccessModifier("Internal", "~");

        public static AccessModifier GetAccessModifier(MemberInfo memberInfo)
        {
            if (memberInfo.IsPrivate())
                return Private;

            if (memberInfo.IsPublic())
                return Public;

            if (memberInfo.IsInternal())
                return Internal;
            
            return Protected;
        }

        public static AccessModifier GetAccessModifier(MethodInfo methodInfo)
        {
            if (methodInfo.IsPrivate)
                return Private;

            if (methodInfo.IsPublic)
                return Public;

            if (methodInfo.IsAssembly)
                return Internal;

            return Protected;
        }
    }
}