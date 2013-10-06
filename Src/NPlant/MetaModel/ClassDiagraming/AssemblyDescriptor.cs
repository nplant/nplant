using System.Reflection;
using NPlant.Core;

namespace NPlant.MetaModel.ClassDiagraming
{
    public class AssemblyDescriptor : IKeyedItem
    {
        public AssemblyDescriptor(Assembly assembly)
        {
            Assembly = assembly;
        }

        public Assembly Assembly { get; private set; }
        public string Key { get { return Assembly.FullName; } }
    }
}