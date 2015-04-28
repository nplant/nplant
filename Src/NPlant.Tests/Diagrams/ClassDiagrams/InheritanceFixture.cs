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
            var simulation = new SimulatedClassDiagramGenerator(new SimpleDiagram());

            simulation.Generate();

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

        [Test]
        public void Interface_Class_Test()
        {
            var simulation = new SimulatedClassDiagramGenerator(new ClassDiagram(typeof(Foo)));
            simulation.Generate();

            Assert.That(simulation.Classes.Count, Is.EqualTo(3));
            Assert.That(simulation.Classes[0].Name, Is.EqualTo("Foo"));
            Assert.That(simulation.Classes[1].Name, Is.EqualTo("Bar"));
            Assert.That(simulation.Classes[2].Name, Is.EqualTo("IBaz"));
        }

        public interface IBaz { }

        public abstract class Bar : IBaz { }

        public class Foo : Bar { }
    }
}