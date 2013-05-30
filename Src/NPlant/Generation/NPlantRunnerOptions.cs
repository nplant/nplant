namespace NPlant.Generation
{
    public interface INPlantRunnerOptions
    {
        string OutputDirectory { get; set; }
        string AssemblyToScan { get; set; }
        string Clean { get; set; }
    }

    public class NPlantRunnerOptions : INPlantRunnerOptions
    {
        public string OutputDirectory { get; set; }

        public string AssemblyToScan { get; set; }

        public string Clean { get; set; }
    }
}