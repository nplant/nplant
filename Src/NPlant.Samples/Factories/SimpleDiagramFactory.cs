using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NPlant.Samples.Factories
{
    public class SimpleDiagramFactory : IDiagramFactory
    {
        public IEnumerable<IDiagram> GetDiagrams()
        {
            return new[]
                {
                    new ClassDiagram(typeof (Foo)).Named("FactoriedFoo1"),
                    new ClassDiagram(typeof (Foo)).Named("FactoriedFoo2"),
                    new ClassDiagram(typeof (Foo)).Named("FactoriedFoo3")
                };
        }
    }

    public class Foo
    {
        public string SomeField;
    }
}
