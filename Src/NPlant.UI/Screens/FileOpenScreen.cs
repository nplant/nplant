using System.Windows.Forms;

namespace NPlant.UI.Screens
{
    public class FileOpenScreen : IResultScreen<FileDialogResult>
    {
        private FileDialogResult _result;
        private readonly string _filter;

        public FileOpenScreen(string filter)
        {
            _filter = filter;
        }

        public FileDialogResult GetResult()
        {
            return _result;
        }

        public DialogResult ShowDialog(IWin32Window owner)
        {
            var dialog = new OpenFileDialog
            {
                RestoreDirectory = true,
                Filter = _filter                         
            };

            var result = dialog.ShowDialog(owner);

            _result = new FileDialogResult(result, dialog.FileName);

            return result;
        }
    }
}