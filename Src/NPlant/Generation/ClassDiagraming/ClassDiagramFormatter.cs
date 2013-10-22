using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPlant.Core;
using NPlant.MetaModel.ClassDiagraming;

namespace NPlant.Generation.ClassDiagraming
{
    public class ClassDiagramFormatter : IDiagramFormatter
    {
        private readonly StringBuilder _buffer = new StringBuilder();
        private readonly ClassDiagramVisitorContext _context;
        private readonly ClassDiagram _diagram;

        public ClassDiagramFormatter(ClassDiagram diagram, ClassDiagramVisitorContext context)
        {
            _diagram = diagram;
            _context = context;
        }

        public string Format()
        {
            _buffer.AppendLine("@startuml");

            WriteTitle(_diagram.Title);

            List<string> unpackaged = new List<string>();
            Dictionary<string, List<string>> packageMap = new Dictionary<string, List<string>>();

            foreach (var rootClass in _diagram.RootClasses.InnerList)
            {
                string classDefinition = WriteClassDefinition(rootClass);

                AssignToPackage(rootClass, packageMap, classDefinition, unpackaged);
            }

            foreach (var relatedClass in _context.VisitedRelatedClasses)
            {
                string classDefinition = WriteClassDefinition(relatedClass);

                AssignToPackage(relatedClass, packageMap, classDefinition, unpackaged);
            }

            foreach (var relatedClass in _context.VisitedRelatedClasses)
            {
                string classDefinition = WriteClassDefinition(relatedClass);

                AssignToPackage(relatedClass, packageMap, classDefinition, unpackaged);
            }

            WritePackages(packageMap, unpackaged);

            _buffer.AppendLine();
            _buffer.AppendLine();

            // write all relationships
            foreach (var relationship in _context.Relationships)
            {
                WriteClassRelationship(relationship);
            }

            // note:  this legend stuff isn't working at the PlantUML level for me... might need to back this out
            WriteLegend(_diagram.Legend);

            WriteNotes(_diagram.Notes);

            _buffer.AppendLine("@enduml");

            return _buffer.ToString();
        }

        private void AssignToPackage(ClassDescriptor rootClass, Dictionary<string, List<string>> packageMap, string classDefinition, List<string> unpackaged)
        {
            foreach (var package in _diagram.Packages)
            {
                if (package.IsMatch(rootClass))
                {
                    AppendClassToPackageMap(packageMap, package.Name, classDefinition);
                    return;
                }
            }

            unpackaged.Add(classDefinition);
        }

        private void WritePackages(Dictionary<string, List<string>> packageMap, IEnumerable<string> unpackaged)
        {
            foreach (var key in packageMap.Keys)
            {
                var package = packageMap[key];

                _buffer.AppendLine(string.Format("package \"{0}\"{1}", key, "{"));

                foreach (var classDefinition in package)
                {
                    _buffer.AppendLine(classDefinition);
                }

                _buffer.AppendLine(string.Format("{0}", "}"));
            }

            foreach (var item in unpackaged)
            {
                _buffer.AppendLine(item);
            }
        }

        private void AppendClassToPackageMap(Dictionary<string, List<string>> packageMap, string package, string classDefinition)
        {
            List<string> classDefinitions;
            
            if (!packageMap.TryGetValue(package, out classDefinitions))
            {
                classDefinitions = new List<string>();
                packageMap.Add(package, classDefinitions);
            }

            classDefinitions.Add(classDefinition);
        }

        private void WriteNotes(IEnumerable<ClassDiagramNote> notes)
        {
            if (notes != null)
            {
                var notesArray = notes.ToArray();

                for (int i = 0; i < notesArray.Length; i++)
                {
                    ClassDiagramNote note = notesArray[i];

                    string lines = string.Join("\\n", note.Lines);

                    string key = "N" + i;
                    _buffer.AppendLine("note \"{0}\" as {1}".FormatWith(lines, key));

                    foreach (var connection in note.ConnectedClasses)
                    {
                        ClassDescriptor descriptor = new ReflectedClassDescriptor(connection);

                        _buffer.AppendLine("{0} .. {1}".FormatWith(descriptor.Name, key));
                    }
                }
            }
        }

        private void WriteLegend(ClassDiagramLegend legend)
        {
            if (legend != null)
            {
                _buffer.AppendLine("legend {0}".FormatWith(legend.Position));
                _buffer.AppendLine("    {0}".FormatWith(legend.Text));
                _buffer.AppendLine("endlegend");
            }
        }

        private void WriteTitle(string title)
        {
            if (!title.IsNullOrEmpty())
            {
                _buffer.AppendLine("title {0}".FormatWith(title));
                _buffer.AppendLine("end title");
            }
        }

        private void WriteClassRelationship(ClassDiagramRelationship relationship)
        {
            string suffix = "";
            string arrow = "";

            switch (relationship.RelationshipType)
            {
                case ClassDiagramRelationshipTypes.Base:
                    arrow = "-up-|>";
                    suffix = " : Extends";
                    break;
                case ClassDiagramRelationshipTypes.HasA:
                    arrow = "*--";
                    suffix = " : Has A \\n{0}".FormatWith(relationship.Name);
                    break;
                case ClassDiagramRelationshipTypes.HasMany:
                    arrow = "*--";
                    suffix = " : Has Many \\n{0}".FormatWith(relationship.Name);
                    break;
                default:
                    throw new NPlantException("Unrecognized relationship type:  {0}".FormatWith(relationship));
            }

            _buffer.AppendLine("{0} {1} {2}{3}".FormatWith(relationship.Party1.Name, arrow, relationship.Party2.Name, suffix));
        }

        private string WriteClassDefinition(ClassDescriptor @class)
        {
            string color = @class.Color ?? _diagram.GetClassColor(@class) ?? null;

            StringBuilder buffer = new StringBuilder();

            buffer.AppendLine(string.Format("    class {0}{1} {2}", @class.Name, color, "{"));

            var definedMembers = @class.Members.InnerList.Where(x => !x.IsInherited).OrderBy(x => x.Name).ToArray();

            if (!IsBaseClassVisible(@class))
            {
                var inheritedMembers = @class.Members.InnerList.Where(x => x.IsInherited).OrderBy(x => x.Name);
                WriteClassMembers(inheritedMembers, buffer);

                if (definedMembers.Length > 0)
                {
                    buffer.AppendLine("    --");
                }
            }

            WriteClassMembers(definedMembers, buffer);

            buffer.AppendLine("    }");

            var note = @class.MetaModel.Note != null ? @class.MetaModel.Note.ToString() : null;

            if (note != null)
            {
                buffer.AppendLine(note);
            }

            return buffer.ToString();
        }

        private bool IsBaseClassVisible(ClassDescriptor @class)
        {
            if (_diagram.RootClasses.InnerList.Any(x => x.ReflectedType == @class.ReflectedType.BaseType))
                return true;

            if (_context.VisitedRelatedClasses.Any(x => x.ReflectedType == @class.ReflectedType.BaseType))
                return true;

            return false;
        }

        private static void WriteClassMembers(IEnumerable<ClassMemberDescriptor> members, StringBuilder buffer)
        {
            foreach (var member in members)
            {
                if(!member.MetaModel.Hidden && member.MetaModel.IsPrimitive)
                    buffer.AppendLine("    {0} {1}".FormatWith(member.MetaModel.Name, member.Name));
            }
        }
    }
}