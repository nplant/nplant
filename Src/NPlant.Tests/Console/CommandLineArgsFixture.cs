using NPlant.Console;
using NUnit.Framework;

namespace NPlant.Tests.Console
{
    [TestFixture]
    public class CommandLineArgsFixture
    {
        [Test]
        public void No_Args_Should_Do_Nothing()
        {
            var args = new CommandLineArgs(new string[] { "--assembly:Foo.dll"});
            Assert.That(args, Is.Not.Null);
        }
    }
}
