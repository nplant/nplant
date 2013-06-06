namespace NPlant.UI.Screens.FileViews
{
    public interface IAssemblyFileView : IFileView
    {
        LoadedDiagram[] Diagrams { get; set; }
        LoadedDiagram SelectedDiagram { get; }
    }
}