using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPlant.Generation.ClassDiagraming;

namespace NPlant.MetaModel.ClassDiagraming
{
    public class ClassWriter : IDescriptorWriter
    {
        private readonly ClassDiagram _diagram;
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

            if (_class.ReflectedType.IsInterface)
                buffer.AppendLine(string.Format("    interface \"{0}\"{1} {2}", _class.Name, color, "{"));
            else if (_class.ReflectedType.IsAbstract)
                buffer.AppendLine(string.Format("    abstract class \"{0}\"{1} {2}", _class.Name, color, "{"));
            else 
                buffer.AppendLine(string.Format("    class \"{0}\"{1} {2}", _class.Name, color, "{"));

            var definedMembers = _class.Members.InnerList.Where(x => !x.IsInherited).OrderBy(x => x.Name).ToArray();

            if (!IsBaseClassVisible(_class, context))
            {
                var inheritedMembers = _class.Members.InnerList.Where(x => x.IsInherited).OrderBy(x => x.Name).ToArray();
                WriteClassMembers(inheritedMembers, buffer);

                if (definedMembers.Length > 0 && inheritedMembers.Length > 0)
                {
                    buffer.AppendLine("    --");
                }
            }

            WriteClassMembers(definedMembers, buffer);
            WriteClassMethods(_class.Methods.InnerList, buffer);

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
            foreach (var member in members.Where(member => member.IsVisible))
            {
                if (member.MetaModel.IsPrimitive || member.TreatAsPrimitive)
                {
                    string accessModifier = member.AccessModifier.Notation;
                    string typeName = member.MetaModel.Name;
                    string memberName = member.Name;

                    buffer.AppendLine("    {0}{1} {2}".FormatWith(accessModifier, typeName, memberName));
                }
                else if (member.MetaModel.IsComplexType && member.MemberType.IsEnumerable())
                {
                    if (member.MemberType.GetEnumeratorType().IsPrimitive || member.MemberType.GetEnumeratorType() == typeof(String))
                    {
                        string accessModifier = member.AccessModifier.Notation;
                        string typeName = member.MetaModel.Name;
                        string memberName = member.Name;

                        buffer.AppendLine("    {0}{1} {2}".FormatWith(accessModifier, typeName, memberName));
                    }
                }
            }
        }

        private void WriteClassMethods(IEnumerable<ClassMethodDescriptor> methods, StringBuilder buffer)
        {
            if (methods != null)
            {
                foreach (var method in methods)
                {
                    string accessModifier = method.AccessModifier.Notation;
                    string methodName = method.Name;
                    string args = method.Arguments;

                    buffer.AppendLine("    {0}{1}({2})".FormatWith(accessModifier, methodName, args));
                }
            }
        }
    }
}