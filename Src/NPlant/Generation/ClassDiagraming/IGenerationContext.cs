namespace NPlant.Generation.ClassDiagraming
{
    public interface IGenerationContext
    {
        void WriteLine(string line);
        string Complete();
    }
}