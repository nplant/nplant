using System.IO;

namespace NPlant.Generation.ClassDiagraming
{
    public class FileClassDiagramGenerator : ClassDiagramGenerator
    {
        private readonly StreamWriter _file;

        public FileClassDiagramGenerator(ClassDiagram definition, StreamWriter file) : base(definition)
        {
            _file = file;
        }

        protected override void Finalize(ClassDiagramVisitorContext current)
        {
            var formatter = Definition.CreateFormatter(ClassDiagramVisitorContext.Current);
            _file.Write(formatter.Format());
        }
    }
}