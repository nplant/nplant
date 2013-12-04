using System;
using NUnit.Framework;

namespace NPlant.Tests
{
    [TestFixture]
    public class StringExtensionMethodsFixture
    {
        [TestCase("abcdefghijk", "g", "abcdef")]
        [TestCase("abcdefghijk", "k", "abcdefghij")]
        [TestCase("abcdefghijk", "a", "")]
        [TestCase("abcdefghijk", "l", "abcdefghijk")]
        [TestCase("abcdefghijk", "", "abcdefghijk")]
        [TestCase("abcdefghijk", null, "abcdefghijk")]
        [TestCase(null, null, null)]
        [TestCase("abcd", null, "abcd")]
        [TestCase(null, "abcd", null)]
        public void SubstringTo_Suit(string source, string to, string expected)
        {
            Assert.That(source.SubstringTo(to), Is.EqualTo(expected));
        }

        [TestCase(typeof(string), "String")]
        [TestCase(typeof(TestSubject1<Arg1>), "TestSubject1<Arg1>")]
        [TestCase(typeof(TestSubject2<Arg1, Arg2>), "TestSubject2<Arg1,Arg2>")]
        [TestCase(typeof(TestSubject3<Arg1,Arg2,Arg3>), "TestSubject3<Arg1,Arg2,Arg3>")]
        public void GetFriendlyGenericName_Suite(Type type, string expected)
        {
            Assert.That(type.GetFriendlyGenericName(), Is.EqualTo(expected));
        }

        public class TestSubject1<T1>
        {
            public T1 Argument1;
        }

        public class TestSubject2<T1,T2>
        {
            public T1 Argument1;
            public T2 Argument2;
        }

        public class TestSubject3<T1,T2,T3>
        {
            public T1 Argument1;
            public T2 Argument2;
            public T3 Argument3;
        }

        public class Arg1{}
        public class Arg2{}
        public class Arg3{}
    }
}
