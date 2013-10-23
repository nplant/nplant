namespace NPlant.Generation
{
    public interface INPlantRunnerOptions
    {
        string OutputDirectory { get; set; }
        string AssemblyToScan { get; set; }
        string Clean { get; set; }
        string Categorize { get; set; }
        string JavaPath { get; set; }
        string PlantUml { get; set; }
    }
}