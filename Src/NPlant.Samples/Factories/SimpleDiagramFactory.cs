using System.Collections.Generic;

namespace NPlant.Samples.Factories
{
    [Sample]
    public class SimpleDiagramFactory : IDiagramFactory
    {
        public IEnumerable<ClassDiagram> GetDiagrams()
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
