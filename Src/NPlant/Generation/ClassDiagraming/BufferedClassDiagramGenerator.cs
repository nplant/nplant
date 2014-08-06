using System.Text;

namespace NPlant.Generation.ClassDiagraming
{
    public class BufferedClassDiagramGenerator : ClassDiagramGenerator
    {
        private readonly StringBuilder _buffer;

        public BufferedClassDiagramGenerator(ClassDiagram definition, StringBuilder buffer) : base(definition)
        {
            _buffer = buffer;
        }

        protected override void Finalize(ClassDiagramVisitorContext current)
        {
            var formatter = Definition.CreateFormatter(ClassDiagramVisitorContext.Current);
            _buffer.Append(formatter.Format());
        }

        public static string GetDiagramText(ClassDiagram diagram)
        {
            var buffer = new StringBuilder();
            var generator = new BufferedClassDiagramGenerator(diagram, buffer);
            
            generator.Generate();

            return buffer.ToString();
        }
    }
}