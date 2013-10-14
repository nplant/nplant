using System.Collections.Generic;
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

            // write all the root classes
            foreach (var rootClass in _diagram.RootClasses.InnerList)
            {
                WriteClassDefinition(rootClass);
            }

            _buffer.AppendLine();
            _buffer.AppendLine();

            // write all the related classes
            foreach (var relatedClass in _context.VisitedRelatedClasses)
            {
                WriteClassDefinition(relatedClass);
            }

            _buffer.AppendLine();
            _buffer.AppendLine();

            // write all relationships
            foreach (var relationship in _context.Relationships)
            {
                WriteClassRelationship(relationship);
            }

            _buffer.AppendLine("@enduml");

            return _buffer.ToString();
        }

        private void WriteClassRelationship(ClassDiagramRelationship relationship)
        {
            string suffix = "";
            string arrow = "";

            switch (relationship.RelationshipType)
            {
                case ClassDiagramRelationshipTypes.Base:
                    arrow = "<|--";
                    suffix = " : Extends {0}".FormatWith(relationship.Party2.Name);
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

        private void WriteClassDefinition(AbstractClassDescriptor @class)
        {
            string color = @class.Color ?? _diagram.GetClassColor(@class) ?? null;

            _buffer.AppendLine(string.Format("    class {0}{1} {2}", @class.Name, color, "{"));

            WriteClassMembers(@class.Members.InnerList);

            _buffer.AppendLine("    }");

            var note = @class.MetaModel.Note != null ? @class.MetaModel.Note.ToString() : null;

            if (note != null)
            {
                _buffer.AppendLine(note);
            }
        }

        private void WriteClassMembers(IEnumerable<ClassMemberDescriptor> members)
        {
            foreach (var member in members)
            {
                if(!member.MetaModel.Hidden && member.MetaModel.IsPrimitive)
                    _buffer.AppendLine("    {0} {1}".FormatWith(member.MetaModel.Name, member.Name));
            }
        }
    }
}