using System.IO;
using System.Windows.Forms;

namespace NPlant.UI
{
    public class FileViewTab : TabPage
    {
        public FileViewTab(string filePath, FileView control)
        {
            control.CheckForNullArg("control");

            this.ToolTipText = filePath;
            this.Text = Path.GetFileName(filePath);

            control.Dock = DockStyle.Fill;

            this.Controls.Add(control);
        }
    }
}