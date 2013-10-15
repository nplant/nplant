using System;
using System.Collections.Generic;

namespace NPlant
{
    public class ClassDiagramNote
    {
        private readonly ClassDiagram _diagram;
        private readonly List<string> _lines = new List<string>();
        private readonly List<Type> _connectedTypes = new List<Type>();

        public ClassDiagramNote(string line, ClassDiagram diagram)
        {
            _diagram = diagram;
            _lines.Add(line);
        }

        public ClassDiagramNote AddLine(string line)
        {
            _lines.Add(line);
            return this;
        }

        public ClassDiagramNote ConnectedToClass<T>()
        {
            if (!_connectedTypes.Contains(typeof(T)))
                _connectedTypes.Add(typeof (T));
    
            return this;
        }

        public ClassDiagram Diagram { get { return _diagram; } }

        internal IEnumerable<string> Lines { get { return _lines; } }

        internal IEnumerable<Type> ConnectedClasses
        {
            get { return _connectedTypes; }
        }
    }
}