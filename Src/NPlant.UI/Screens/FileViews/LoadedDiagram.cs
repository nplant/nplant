using NPlant.Core;

namespace NPlant.UI
{
    public class LoadedDiagram
    {
        private readonly IDiagram _diagram;
        private readonly string _name;

        public LoadedDiagram(IDiagram diagram)
        {
            _diagram = diagram;
            _name = _diagram.GetName();
        }

        public IDiagram Diagram
        {
            get { return _diagram; }
        }

        public override string ToString()
        {
            return _name;
        }
    }
}