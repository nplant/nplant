using System;
using NPlant.MetaModel.ClassDiagraming;
using NPlant.Samples.CIrcularReferfences;
using NUnit.Framework;

namespace NPlant.Tests.Diagrams.ClassDiagrams
{
    [TestFixture]
    public class InterfaceScenarios
    {
        [Test]
        public void Circular_References_Dont_Create_Multiple_Classes()
        {
            var simulation = new ClassDiagramSimulation(new Diagram());
            simulation.Simulate();

            Assert.That(simulation.Classes.Count, Is.EqualTo(3));
            Assert.That(simulation.Interfaces.Count, Is.EqualTo(1));
            Assert.That(simulation.Interfaces["IDisposable"], Is.Not.Null);

            Assert.That(simulation.Classes["Foo"].Members["SomeString"].MemberType, Is.EqualTo(typeof(string)));
            Assert.That(simulation.Classes["Foo"].Members["TheBar"].MemberType, Is.EqualTo(typeof(Bar)));
            Assert.That(simulation.Classes["Foo"].Extensions["IDisposable"].AsInterface(), Is.True);

            Assert.That(simulation.Classes["Bar"].Members["SomeDate"].MemberType, Is.EqualTo(typeof(DateTime?)));

            Assert.That(simulation.Classes["Baz"].Members["TheFoo"].MemberType, Is.EqualTo(typeof(Foo)));
        }

        public class Diagram : ClassDiagram
        {
            public Diagram()
            {
                base.Add<Foo>();
                base.Options.ImplementersOf<IDisposable>().Show();
            }
        }

        public class Foo : IDisposable
        {
            public string SomeString;
            public Bar TheBar;
            public Baz TheBaz;

            public void Dispose()
            {
                
            }
        }

        public class Bar
        {
            public DateTime? SomeDate;

        }

        public class Baz
        {
            public Foo TheFoo;
        }

    }
}