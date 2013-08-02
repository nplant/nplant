using System;
using System.Windows.Forms;

namespace NPlant.UI.Screens
{
    public partial class SettingsScreen : Form, ISettingScreen
    {
        private readonly SettingsScreenController _controller;

        public SettingsScreen()
        {
            InitializeComponent();
            _controller = new SettingsScreenController(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _controller.Start();
        }

        private void OnOKButtonClick(object sender, EventArgs e)
        {
            using(new WaitCursor())
                _controller.SaveChanges();
            
            this.Close();
        }

        private void OnCancelButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnJavaHomeButtonClick(object sender, EventArgs e)
        {
            var result = ScreenManager.Launch<FileOpenScreen, FileDialogResult>(this, () => new FileOpenScreen(FileFilters.ExecutableFileFilter));

            if(result.UserApproved)
                JavaHomeTextBox.Text = result.FilePath;
        }

        public string JavaPath 
        {
            get { return JavaHomeTextBox.Text; }
            set { JavaHomeTextBox.Text = value; }
        }
    }
}
