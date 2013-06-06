using System.Windows.Forms;

namespace NPlant.UI
{
    public class FileOpenScreen : IResultScreen<FileOpenResult>
    {
        private FileOpenResult _result;

        public FileOpenResult GetResult()
        {
            return _result;
        }

        public DialogResult ShowDialog(IWin32Window owner)
        {
            var dialog = new OpenFileDialog
            {
                Filter = ".NET Assembly Files (*.dll;*.exe)|" +
                         "*.dll;*.exe|" +
                         "NPlant Files (.nplant)|" +
                         "*.nplant"
                         
            };

            var result = dialog.ShowDialog(owner);

            _result = new FileOpenResult(result, dialog.FileName);

            return result;
        }
    }

    public class FileOpenResult
    {
        public FileOpenResult(DialogResult result, string fileName)
        {
            this.UserApproved = result == DialogResult.OK;
            this.FilePath = fileName;
        }

        public string FilePath { get; private set; }

        public bool UserApproved { get; private set; }
    }
}