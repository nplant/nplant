using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace NPlant.Tests.Reflection
{
    [TestFixture]
    public class GetEnumeratorTypeFixture
    {
        [TestCase(typeof(object[]), typeof(object))]
        [TestCase(typeof(string[]), typeof(string))]
        [TestCase(typeof(FooList), typeof(Foo))]
        [TestCase(typeof(List<string>), typeof(string))]
        [TestCase(typeof(List<Foo>), typeof(Foo))]
        [TestCase(typeof(IList<Foo>), typeof(Foo))]
        public void Suite(Type input, Type expected)
        {
            Assert.That(input.GetEnumeratorType(), Is.EqualTo(expected));
        }

        [TestCase(typeof(object), typeof(object))]
        public void Exceptionals(Type input, Type expected)
        {
            Assert.Throws<ArgumentException>(() => Assert.That(input.GetEnumeratorType(), Is.EqualTo(expected)));
        }

        internal class FooList : List<Foo>{}

        internal class Foo{}
    }
}
