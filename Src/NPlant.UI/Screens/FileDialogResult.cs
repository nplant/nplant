using System.Windows.Forms;

namespace NPlant.UI.Screens
{
    public class FileDialogResult
    {
        public FileDialogResult(DialogResult result, string fileName)
        {
            this.UserApproved = result == DialogResult.OK;
            this.FilePath = fileName;
        }

        public string FilePath { get; private set; }

        public bool UserApproved { get; private set; }
    }
}