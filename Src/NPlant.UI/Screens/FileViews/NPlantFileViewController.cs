using System.IO;
using System.Windows.Forms;

namespace NPlant.UI.Screens.FileViews
{
    public class NPlantFileViewController : FileViewController
    {
        private readonly INPlantFileView _view;
        private readonly string _filePath;

        public NPlantFileViewController(INPlantFileView view, string filePath) : base(view)
        {
            _view = view;
            _filePath = filePath;
        }

        public void Start()
        {
            _view.DiagramTextChanged(OnDiagramTextChanged);

            string text = null;

            if (File.Exists(_filePath))
                text = File.ReadAllText(_filePath);

            _view.DiagramText = text;
        }

        protected void OnDiagramTextChanged(string text)
        {
            this.Save();
            this.Generate();
        }

        public void Save()
        {
            File.WriteAllText(_filePath, _view.DiagramText);
        }

        public void Copy()
        {
            Clipboard.SetText(_view.DiagramText);
        }

        protected override string GetFilePath()
        {
            return _filePath;
        }
    }
}