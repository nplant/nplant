using NPlant.Core;
using NPlant.UI;
using NUnit.Framework;

namespace NPlant.Tests.UI
{
    [TestFixture]
    public class CommandLineArgumentsFixture
    {
        [Test]
        public void Load_With_No_Arguments_Returns_And_Empty_Instance()
        {
            var args = CommandLineArguments.Load(new string[] { });
            Assert.That(args.FilePath, Is.Null);
            Assert.That(args.FilePath.IsNPlantFilePath(), Is.False);
            Assert.That(args.FilePath.IsAssemblyFilePath(), Is.False);
        }
        
        [Test]
        public void Load_With_Null_Arguments_Returns_And_Empty_Instance()
        {
            var args = CommandLineArguments.Load(null);
            Assert.That(args.FilePath, Is.Null);
            Assert.That(args.FilePath.IsNPlantFilePath(), Is.False);
            Assert.That(args.FilePath.IsAssemblyFilePath(), Is.False);
        }

        [Test]
        public void Can_Load_NPlant_File_Path()
        {
            var args = CommandLineArguments.Load(new[] { "Foo.nplant" });
            Assert.That(args.FilePath, Is.EqualTo("Foo.nplant"));
            Assert.That(args.FilePath.IsNPlantFilePath(), Is.True);
            Assert.That(args.FilePath.IsAssemblyFilePath(), Is.False);
        }

        [Test]
        public void Can_Load_Dll_File_Path()
        {
            var args = CommandLineArguments.Load(new[] { "Foo.dll" });
            Assert.That(args.FilePath, Is.EqualTo("Foo.dll"));
            Assert.That(args.FilePath.IsNPlantFilePath(), Is.False);
            Assert.That(args.FilePath.IsAssemblyFilePath(), Is.True);
        }

        [Test]
        public void Can_Load_Exe_File_Path()
        {
            var args = CommandLineArguments.Load(new[] { "Foo.exe" });
            Assert.That(args.FilePath, Is.EqualTo("Foo.exe"));
            Assert.That(args.FilePath.IsNPlantFilePath(), Is.False);
            Assert.That(args.FilePath.IsAssemblyFilePath(), Is.True);
        }

        [Test]
        public void Other_Extension_Types_Should_Throw()
        {
            Assert.Throws<NPlantException>(() => CommandLineArguments.Load(new[] {"Foo.bar"}));
        }
    }
}
