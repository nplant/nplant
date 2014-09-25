using System;
using System.Reflection;
using NPlant.Core;
using NPlant.MetaModel.ClassDiagraming;
using NUnit.Framework;

namespace NPlant.Tests.Diagraming
{
    [TestFixture]
    public class ClassMethodDescriptorFixture
    {
        [Test]
        public void Simple_Parameterless_Void_Method_Should_Return_The_Method_Name()
        {
            MethodInfo doSomething = ReflectOn<Subject>.ForMethod(x => x.DoSomething());
            ClassMethodDescriptor descriptor = new ClassMethodDescriptor(doSomething);

            Assert.That(descriptor.Key, Is.EqualTo("DoSomething()"));
        }

        [Test]
        public void Void_With_Parameters_Method_Should_Return_The_Method_Name_And_Coma_Delimited_List_Of_Parameter_Types()
        {
            MethodInfo doSomething = ReflectOn<Subject>.ForMethod(x => x.DoSomething(null, null, null));
            ClassMethodDescriptor descriptor = new ClassMethodDescriptor(doSomething);

            Assert.That(descriptor.Key, Is.EqualTo("DoSomething(String, Nullable<DateTime>, Subject)"));
        }

        [Test]
        public void Ignore_Return_Type()
        {
            MethodInfo doSomething = ReflectOn<Subject>.ForMethod(x => x.DoSomething(null));
            ClassMethodDescriptor descriptor = new ClassMethodDescriptor(doSomething);

            Assert.That(descriptor.Key, Is.EqualTo("DoSomething(String)"));
        }

        public class Subject
        {
            public void DoSomething() { }
            public void DoSomething(string parm1, DateTime? parm2, Subject parm3) { }
            public Subject DoSomething(string parm1) { return null; }
        }
    }
}
