using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using NPlant.Generation;

namespace NPlant.UI.Screens.FileViews
{
    public abstract class FileViewController
    {
        private readonly IFileView _view;

        protected FileViewController(IFileView view)
        {
            _view = view;
        }

        public void Generate()
        {
            var filePath = GetDiagramText();
            var model = ImageFileGenerationModel.Create(filePath);

            _view.Image = NPlantImage.Create(model.DiagramText, model.JavaPath, model.GetJavaArguments());
        }

        protected abstract string GetDiagramText();
    }
}