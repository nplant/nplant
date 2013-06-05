using System.IO;

namespace NPlant.UI
{
    public class NPlantFileViewController
    {
        private readonly INPlantFileView _view;
        private readonly string _filePath;

        public NPlantFileViewController(INPlantFileView view, string filePath)
        {
            _view = view;
            _filePath = filePath;
        }

        public void Start()
        {
            string text = null;

            if (File.Exists(_filePath))
                text = File.ReadAllText(_filePath);

            _view.DiagramText = text;
        }
    }
}