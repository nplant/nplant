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

            var npImage = new NPlantImage(model.JavaPath, model.Invocation)
            {
                Logger = msg => EventDispatcher.Raise(new UserNotificationEvent(msg))
            };

            _view.Image = npImage.Create(model.DiagramText);
        }

        protected abstract string GetDiagramText();
    }
}