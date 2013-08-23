using System;

namespace NPlant.UI.Screens.FileViews
{
    public interface IClassDiagramView : IFileView
    {
        LoadedDiagram[] Diagrams { get; set; }
        LoadedDiagram SelectedDiagram { get; }
        string DiagramText { get; set; }
        bool ShowDiagramClassesPanel { get; set; }
    }
}