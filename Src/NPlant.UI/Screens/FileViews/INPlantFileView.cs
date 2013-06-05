using System;

namespace NPlant.UI
{
    public interface INPlantFileView : IView
    {
        string DiagramText { get; set; }
        void DiagramTextChanged(Action<string> action);
    }
}