using System;

namespace NPlant.Generation
{
    public interface IRunnerRecorder : IDisposable
    {
        void Record(string filePath);
        void Log(string message);
    }
}