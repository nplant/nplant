using System;

namespace NPlant.UI
{
    public interface INPlantFileView : IView
    {
        string DiagramText { get; set; }
        bool GenerateOnTextChange { get; set; }
        int Progress { get; set; }
        void DiagramTextChanged(Action<string> action);
    }
}