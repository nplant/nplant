using System;
using NPlant.MetaModel.ClassDiagraming;
using NUnit.Framework;

namespace NPlant.Tests.Diagrams.ClassDiagrams
{
    [TestFixture]
    public class InheritanceFixture
    {
        [Test]
        public void Base_Classs_Will_Be_Rendered_By_Default()
        {
            var simulation = new ClassDiagramSimulation(new SimpleDiagram());

            simulation.Simulate();

            Assert.That(simulation.Classes.Count, Is.EqualTo(4));
        }

        public class SimpleDiagram : ClassDiagram
        {
            public SimpleDiagram()
            {
                base.AddClass<SubSimpleEntity>();
                base.AddClass<SecondSubSimpleEntity>();
                base.AddClass<SubSubSimpleEntity>();
            }
        }

        public class SimpleEntity
        {
            public string Foo;
            public string Bar;
            public string Baz;
        }

        public class SubSimpleEntity : SimpleEntity
        {
            public string Bah;
        }
        
        public class SecondSubSimpleEntity : SimpleEntity
        {

        }

        public class SubSubSimpleEntity : SubSimpleEntity
        {
            public string Boo;
        }
    }
}