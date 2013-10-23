using System.IO;
using System.Windows.Forms;

namespace NPlant.UI.Screens
{
    public class FileDialogResult
    {
        public FileDialogResult(DialogResult result, string fileName)
        {
            this.UserApproved = result == DialogResult.OK;
            this.FilePath = fileName;

            if (this.UserApproved && !this.FilePath.IsNullOrEmpty())
            {
                this.FileName = Path.GetFileName(this.FilePath);
                this.DirectoryName = Path.GetDirectoryName(this.FilePath);
            }
        }

        public string FileName { get; private set; }

        public string DirectoryName { get; private set; }
        
        public string FilePath { get; private set; }

        public bool UserApproved { get; private set; }
    }
}