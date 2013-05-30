namespace NPlant.Generation
{
    internal class NullRecorder : IRunnerRecorder
    {
        private NullRecorder() { }

        public void Dispose() { }

        public void Record(string filePath) { }

        public void Log(string message) { }

        public static IRunnerRecorder Instance = new NullRecorder();
    }
}