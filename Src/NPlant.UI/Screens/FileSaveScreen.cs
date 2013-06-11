using System.Windows.Forms;

namespace NPlant.UI.Screens
{
    public class FileSaveScreen : IResultScreen<FileDialogResult>
    {
        private FileDialogResult _result;

        public FileDialogResult GetResult()
        {
            return _result;
        }

        public DialogResult ShowDialog(IWin32Window owner)
        {
            var dialog = new SaveFileDialog {AddExtension = true, OverwritePrompt = true};

            var result = dialog.ShowDialog(owner);

            _result = new FileDialogResult(result, dialog.FileName);

            return result;
        }
    }
}