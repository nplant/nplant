using System.Text;
using NPlant.MetaModel.ClassDiagraming;

namespace NPlant.Generation.ClassDiagraming
{
    public class ClassDiagramGenerationContext : IGenerationContext
    {
        private readonly StringBuilder _builder = new StringBuilder();

        public ClassDiagramGenerationContext(ClassDiagram diagram)
        {
            _builder.AppendLine("@startuml");

            this.Diagram = diagram;
        }

        public ClassDiagram Diagram { get; private set; }

        public IClassDiagramMetaModel MetaModel
        {
            get { return this.Diagram; }
        }

        public void WriteLine(string line)
        {
            _builder.AppendLine(line);
        }

        public string Complete()
        {
            _builder.AppendLine("@enduml");

            return _builder.ToString();
        }
    }
}