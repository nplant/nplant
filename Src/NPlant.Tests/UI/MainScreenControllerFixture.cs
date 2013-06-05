using NPlant.UI;
using NUnit.Framework;

namespace NPlant.Tests.UI
{
    [TestFixture]
    public class MainScreenControllerFixture
    {
        [Test]
        public void Launching_The_App_With_A_File_Path_Should_Append_The_Path_To_The_Window_Title()
        {
            var screen = new StubMainScreen();
            var controller = new MainScreenController(screen, new StubCommandLineArguments("Foo.nplant"));
            controller.Start();

            Assert.That(screen.Title, Is.EqualTo("NPlant UI - Foo.nplant"));
        }

        [Test]
        public void Launching_The_App_With_No_File_Path_Should_Not_Append_The_Path_To_The_Window_Title()
        {
            var screen = new StubMainScreen();
            var controller = new MainScreenController(screen, new StubCommandLineArguments());
            controller.Start();

            Assert.That(screen.Title, Is.EqualTo("NPlant UI"));
        }
    }

    public class StubCommandLineArguments : CommandLineArguments
    {
        public StubCommandLineArguments(string filePath)
            : base(new[] { filePath })
        {
        }
        public StubCommandLineArguments()
            : base(new string[] { })
        {
        }
    }

    public class StubMainScreen : IMainScreen
    {
        public string Title { get; set; }

        public void AddFileView(FileViewType type, string filePath)
        {
            
        }

        public void DisplayUserNotification(UserNotificationEvent @event)
        {
        }
    }
}
