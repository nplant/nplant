using System;
using System.Windows.Forms;

namespace NPlant.UI
{
    public partial class AboutScreen : Form, IScreen
    {
        public AboutScreen()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public T GetResult<T>()
        {
            return default(T);
        }
    }
}
