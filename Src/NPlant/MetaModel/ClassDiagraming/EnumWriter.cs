using System;
using System.Text;
using NPlant.Generation.ClassDiagraming;

namespace NPlant.MetaModel.ClassDiagraming
{
    public class EnumWriter : IDescriptorWriter
    {
        private readonly Type _enumType;

        public EnumWriter(Type enumType)
        {
            _enumType = enumType;
        }

        public string Write(ClassDiagramVisitorContext context)
        {
            StringBuilder buffer = new StringBuilder();

            var names = Enum.GetNames(_enumType);

            buffer.AppendLine("enum {0} {1}".FormatWith(_enumType.Name, "{"));

            foreach (var name in names)
            {
                buffer.AppendLine("     {0}".FormatWith(name));
            }

            buffer.AppendLine("}");

            return buffer.ToString();
        }
    }
}