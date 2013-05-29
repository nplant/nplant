using System;
using System.Collections.Generic;
using NPlant.Tests.Readers;
using NUnit.Framework;

namespace NPlant.Tests.ClassDiagrams
{
    [TestFixture]
    public class AggregationScenarios
    {
        [Test]
        public void GIVEN_A_Simple_Single_Entity_Diagram_WHEN_Rendered_With_Defaults_THEN_Diagram_Has_One_Class_With_Members()
        {
            var reader = ClassDiagramReader.Read(new DefaultBehaviorDiagram());

            Assert.That(reader.Classes.Length, Is.EqualTo(4));
            Assert.That(reader.Classes[0].Name, Is.EqualTo("AggregationEntity"));
        }

        public class DefaultBehaviorDiagram : ClassDiagramDefinition
        {
            public DefaultBehaviorDiagram()
            {
                this.AddClass<AggregationEntity>();
            }
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