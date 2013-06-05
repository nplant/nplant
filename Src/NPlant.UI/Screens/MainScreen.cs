using System;
using System.Windows.Forms;
using NPlant.UI.Screens.FileViews;

namespace NPlant.UI
{
    public partial class MainScreen : Form, IMainScreen
    {
        private readonly MainScreenController _controller;

        public MainScreen()
        {
            InitializeComponent();

            CommandLineArguments args = CommandLineArguments.Load(Environment.GetCommandLineArgs());
            _controller = new MainScreenController(this, args);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _controller.Start();
        }

        public string Title
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

        public void AddFileView(FileViewType type, string filePath)
        {
            FileViewTab tab;
            
            if (type == FileViewType.AssemblyFile)
                tab = new FileViewTab(filePath, new AssemblyFileView());
            else
                tab = new FileViewTab(filePath, new NPlantFileView(filePath));

            this.FileViewTabs.TabPages.Add(tab);
        }

        private void OnExitClick(object sender, EventArgs e)
        {
            _controller.Stop(this.Close);
        }

        private void OnAboutClick(object sender, EventArgs e)
        {
            ScreenManager.Launch<AboutScreen>(this);
        }

        private void OnOpenClick(object sender, EventArgs e)
        {
            var result = ScreenManager.Launch<FileOpenScreen, FileOpenResult>(this);
            _controller.OpenFile(result);
        }
    }

    public enum FileViewType
    {
        NPlantFile,
        AssemblyFile
    }

    public interface IMainScreen
    {
        string Title { get; set; }
        void AddFileView(FileViewType type, string filePath);
    }

    public class MainScreenController
    {
        private readonly IMainScreen _screen;
        private readonly CommandLineArguments _args;

        public MainScreenController(IMainScreen screen, CommandLineArguments args)
        {
            _screen = screen;
            _args = args;
        }

        public void Start()
        {
            if (_args.HasFilePath)
                OpenFile(_args.FilePath);
            else
                _screen.Title = "NPlant UI";
        }

        public void Stop(Action action)
        {
            action();
        }

        public void OpenFile(FileOpenResult result)
        {
            if (result.UserApproved)
            {
                this.OpenFile(result.FilePath);
            }
        }

        private void OpenFile(string filePath)
        {
            _screen.Title = "NPlant UI - {0}".FormatWith(filePath);

            if (filePath.IsNPlantFilePath())
                _screen.AddFileView(FileViewType.NPlantFile, filePath);
            else if (filePath.IsAssemblyFilePath())
                _screen.AddFileView(FileViewType.AssemblyFile, filePath);
        }
    }
}
