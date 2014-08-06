using NPlant.Core;

namespace NPlant.UI
{
    public class LoadedDiagram
    {
        private readonly ClassDiagram _diagram;
        private readonly string _name;

        public LoadedDiagram(ClassDiagram diagram)
        {
            _diagram = diagram;
            _name = _diagram.Name;
        }

        public ClassDiagram Diagram
        {
            get { return _diagram; }
        }

        public override string ToString()
        {
            return _name;
        }
    }
}