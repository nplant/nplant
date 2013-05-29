using System;
using System.Collections.Generic;
using NPlant.MetaModel.ClassDiagraming;
using NUnit.Framework;

namespace NPlant.Tests.ClassDiagrams
{
    [TestFixture]
    public class AggregationScenarios
    {
        [Test]
        public void GIVEN_A_Simple_Single_Entity_Diagram_WHEN_Rendered_With_Defaults_THEN_Diagram_Has_One_Class_With_Members()
        {
            var simulation = new ClassDiagramSimulation(new ClassDiagram(typeof(AggregationEntity)));
            simulation.Simulate();
            
            Assert.That(simulation.Classes.Count, Is.EqualTo(5));
            Assert.That(simulation.Classes[0].Name, Is.EqualTo("AggregationEntity"));
        }
        
        public class AggregationEntity
        {
            public Foo TheFoo;
            public Bar TheBar;
            public BahList TheBahs;
            public Bah[] TheBahsArray;
        }

        public class Bah
        {
            public string SomeProperty;
            public DateTime? SomeNullableDateTime;
        }

        public class BahList : List<Bah>
        {
            
        }

        public class Foo
        {
            public IBar TheBar;
        }

        public interface IBar{}
        
        public class Bar
        {
        }

        public class Baz
        {
            public Foo TheFoo;
        }
    }
}