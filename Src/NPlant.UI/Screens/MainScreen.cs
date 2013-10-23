using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NPlant.UI.Screens;
using NPlant.UI.Screens.FileViews;

namespace NPlant.UI
{
    public partial class MainScreen : Form, IMainScreen
    {
        private readonly MainScreenController _controller;

        public MainScreen()
        {
            InitializeComponent();

            var args = Environment.GetCommandLineArgs();

            CommandLineArguments arguments = CommandLineArguments.Load(args, true);

            _controller = new MainScreenController(this, arguments);
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
            if (!File.Exists(filePath))
            {
                EventDispatcher.Raise(new UserNotificationEvent("File '{0}' not found".FormatWith(filePath), UserNotificationType.Warning));
                return;
            }

            var tab = new FileViewTab(filePath, new ClassDiagramView(filePath));

            tab.ContextMenu = new ContextMenu(new []{new MenuItem("Close", (sender, args) =>
                {
                    MenuItem item = (MenuItem) sender;
                    FileViewTab t = (FileViewTab) item.Tag;
                    RemoveFileView(t);
                }){Tag = tab}
            });

            this.FileViewTabs.TabPages.Add(tab);
            this.FileViewTabs.SelectedTab = tab;
        }

        private void RemoveFileView(FileViewTab fileView)
        {
            bool found = this.FileViewTabs.TabPages.Cast<FileViewTab>().Any(tab => tab == fileView);

            if(found)
                this.FileViewTabs.TabPages.Remove(fileView);

        }

        public void DisplayUserNotification(UserNotificationEvent @event)
        {
            this.ConsoleLogText.AppendText(@event.Message);
            this.ConsoleLogText.SelectionStart = ConsoleLogText.Text.Length;
            this.ConsoleLogText.ScrollToCaret();

            string caption = null;

            if (@event.NotificationType == UserNotificationType.Warning)
                caption = "Warning";
            else if (@event.NotificationType == UserNotificationType.Error)
                caption = "Error";

            if(caption != null)
                MessageBox.Show(@event.Message, caption, MessageBoxButtons.OK);
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
            var result = ScreenManager.Launch<FileOpenScreen, FileDialogResult>(this, () => new FileOpenScreen(FileFilters.OpenFileFilter));
            _controller.OpenFile(result);
        }

        public bool IsConsoleVisible
        {
            get { return showConsoleToolStripMenuItem.Checked; }
            set { showConsoleToolStripMenuItem.Checked = value; }
        }

        private void OnShowConsoleCheckedChanged(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = ! IsConsoleVisible;
        }

        private void OnSettingsMenuItemClick(object sender, EventArgs e)
        {
            ScreenManager.Launch<SettingsScreen>(this);
        }

        private void OnGetGraphvizMenuItemClick(object sender, EventArgs e)
        {
            Process.Start("http://www.graphviz.org");
        }

        private void OnGetJavaMenuItemClick(object sender, EventArgs e)
        {
            Process.Start("http://www.java.com/getjava");
        }

        private void OnGoToPlantUMLMenuItemClick(object sender, EventArgs e)
        {
            Process.Start("http://plantuml.sourceforge.net/");
        }
    }
}
