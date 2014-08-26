using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPlant.Generation.ClassDiagraming;

namespace NPlant.MetaModel.ClassDiagraming
{
    public class ClassWriter : IDescriptorWriter
    {
        private ClassDiagram _diagram;
        private readonly ClassDescriptor _class;

        public ClassWriter(ClassDiagram diagram, ClassDescriptor @class)
        {
            _diagram = diagram;
            this._class = @class;
        }

        public string Write(ClassDiagramVisitorContext context)
        {
            string color = _class.Color ?? _diagram.GetClassColor(_class);

            StringBuilder buffer = new StringBuilder();

            buffer.AppendLine(string.Format("    class \"{0}\"{1} {2}", _class.Name, color, "{"));

            var definedMembers = _class.Members.InnerList.Where(x => !x.IsInherited).OrderBy(x => x.Name).ToArray();

            if (!IsBaseClassVisible(_class, context))
            {
                var inheritedMembers = _class.Members.InnerList.Where(x => x.IsInherited).OrderBy(x => x.Name);
                WriteClassMembers(inheritedMembers, buffer);

                if (definedMembers.Length > 0)
                {
                    buffer.AppendLine("    --");
                }
            }

            WriteClassMembers(definedMembers, buffer);

            buffer.AppendLine("    }");

            var note = _class.MetaModel.Note != null ? _class.MetaModel.Note.ToString() : null;

            if (note != null)
            {
                buffer.AppendLine(note);
            }

            return buffer.ToString();

        }
        private bool IsBaseClassVisible(ClassDescriptor @class, ClassDiagramVisitorContext context)
        {
            if (_diagram.RootClasses.InnerList.Any(x => x.ReflectedType == @class.ReflectedType.BaseType))
                return true;

            if (context.VisitedRelatedClasses.Any(x => x.ReflectedType == @class.ReflectedType.BaseType))
                return true;

            return false;
        }

        private void WriteClassMembers(IEnumerable<ClassMemberDescriptor> members, StringBuilder buffer)
        {
            foreach (var member in members)
            {
                if (!member.MetaModel.Hidden && (member.MetaModel.IsPrimitive || member.TreatAsPrimitive))
                    buffer.AppendLine("    {0}{1} {2}".FormatWith(GetClassMemberAccessModifierCode(member.AccessModifier), member.MetaModel.Name, member.Name));
            }
        }

        private string GetClassMemberAccessModifierCode(AccessModifier am)
        {
            switch(am)
            {
                case AccessModifier.Public:
                    return "+";
                case AccessModifier.Private:
                    return "-";
                case AccessModifier.Protected:
                    return "#";
                case AccessModifier.Internal:
                    return "~";
                default:
                    return "";
            }
        }
    }
}