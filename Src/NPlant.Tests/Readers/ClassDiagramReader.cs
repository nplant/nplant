using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NPlant.Tests.Readers
{
    public class ClassDiagramReader
    {
        private readonly IList<ClassElement> _classes = new List<ClassElement>();

        public static ClassDiagramReader Read(ClassDiagramDefinition diagram)
        {
            var reader = new ClassDiagramReader();
            reader.Load(diagram);

            return reader;
        }

        private void Load(IDiagram diagram)
        {
            string notation = diagram.CreateGenerator().Generate();

            var stringReader = new StringReader(notation);
            Queue<string> lines = stringReader.ReadAllLines();

            while (lines.Count > 0)
            {
                string line = lines.Dequeue();

                if (line.StartsWith("class") && line.EndsWith("{"))
                {
                    ClassElement @class = this.AddClass(line);
                    @class.Load(DequeueTo("}", lines));
                }
            }

        }

        private static IEnumerable<string> DequeueTo(string match, Queue<string> lines)
        {
            var buffer = new List<string>();
            lines.CheckForNullArg("lines");

            while (lines.Count > 0)
            {
                var line = lines.Dequeue();

                if (line == match)
                    break;

                buffer.Add(line);
            }

            return buffer;
        }

        private ClassElement AddClass(string line)
        {
            var @class = new ClassElement(line);
            _classes.Add(@class);

            return @class;
        }

        public ClassElement[] Classes
        {
            get { return _classes.ToArray(); }
        }

        public class ClassElement
        {
            private readonly IList<ClassMemberElement> _members = new List<ClassMemberElement>();

            public ClassElement(string line)
            {
                int start = "class ".Length;
                int end = line.Length - (start + 2);

                if ((start + end) < line.Length)
                {
                    this.Name = line.Substring(start, end);
                }
            }

            public string Name { get; private set; }

            public ClassMemberElement[] Members
            {
                get { return _members.ToArray(); }
            }

            public void Load(IEnumerable<string> lines)
            {
                lines.CheckForNullArg("lines");

                foreach (var line in lines)
                {
                    _members.Add(new ClassMemberElement(line.Trim()));
                }
            }
        }

        public class ClassMemberElement
        {
            public ClassMemberElement(string line)
            {
                string[] split = line.Split(' ');
                
                if (split.Length == 2)
                {
                    this.Type = split[0];
                    this.Name = split[1];
                }
                else if (split.Length == 1)
                {
                    this.Name = split[0];
                }
            }

            public string Type;
            public string Name;
        }
    }
}