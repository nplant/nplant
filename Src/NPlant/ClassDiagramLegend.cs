namespace NPlant
{
    public class ClassDiagramLegend
    {
        private readonly ClassDiagram _diagram;
        private string _position = "center";
        private readonly string _text;

        public ClassDiagramLegend(ClassDiagram diagram, string text)
        {
            _text = text;
            _diagram = diagram;
        }

        public ClassDiagram DisplayLeft()
        {
            _position = "left";
            return _diagram;
        }

        public ClassDiagram DisplayRight()
        {
            _position = "right";
            return _diagram;
        }

        public ClassDiagram DisplayCenter()
        {
            _position = "center";
            return _diagram;
        }

        internal string Position { get { return _position; } }

        internal string Text { get { return _text; } }
    }
}