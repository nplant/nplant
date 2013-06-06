using System;

namespace NPlant.UI.Screens.FileViews
{
    public interface INPlantFileView : IFileView
    {
        string DiagramText { get; set; }
        bool GenerateOnTextChange { get; set; }
        void DiagramTextChanged(Action<string> action);
    }
}