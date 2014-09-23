using System.Linq;
using System.Reflection;
using NPlant.Core;

namespace NPlant.MetaModel.ClassDiagraming
{
    public class ClassMethodDescriptor : IKeyedItem
    {
        public ClassMethodDescriptor(MethodInfo method)
        {
            this.Key = BuildKey(method);
            this.Name = method.Name;
            this.AccessModifier = AccessModifier.GetAccessModifier(method);
        }

        private string BuildKey(MethodInfo method)
        {
            ParameterInfo[] parameters = method.GetParameters();

            if (parameters.Length < 1)
                return "{0}()".FormatWith(method.Name);

            return "{0}({1})".FormatWith(method.Name, string.Join(",", parameters.Select(x => x.ParameterType.GetFriendlyGenericName())));
        }

        public string Key { get; private set; }
        public string Name { get; private set; }
        public AccessModifier AccessModifier { get; private set; }
    }
}