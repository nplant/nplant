using System.Linq;
using System.Reflection;
using NPlant.Core;

namespace NPlant.MetaModel.ClassDiagraming
{
    public class ClassMethodDescriptor : IKeyedItem
    {
        public ClassMethodDescriptor(MethodInfo method)
        {
            this.Arguments = BuildArguments(method);
            this.Key = BuildKey(method, this.Arguments);
            this.Name = method.Name;
            this.AccessModifier = AccessModifier.GetAccessModifier(method);
        }

        private string BuildKey(MethodInfo method, string arguments)
        {
            return "{0}({1})".FormatWith(method.Name, arguments);
        }
        
        private string BuildArguments(MethodInfo method)
        {
            ParameterInfo[] parameters = method.GetParameters();

            if (parameters.Length < 1)
                return null;

            return string.Join(", ", parameters.Select(x => x.ParameterType.GetFriendlyGenericName()));
        }

        public string Key { get; private set; }
        public string Name { get; private set; }
        public AccessModifier AccessModifier { get; private set; }
        public string Arguments { get; private set; }
    }
}