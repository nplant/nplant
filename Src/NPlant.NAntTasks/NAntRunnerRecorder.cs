using System;
using System.Text;
using NAnt.Core;
using NPlant.Generation;

namespace NPlant.NAntTasks
{
    public class NAntRunnerRecorder : IRunnerRecorder
    {
        private readonly Task _task;
        private readonly string _delimiter;
        private readonly StringBuilder _builder = new StringBuilder();
        private int _recordedCount;
        private readonly string _propertyName;
        private string _filePath;

        public NAntRunnerRecorder(Task task, string propertyName, string delimiter)
        {
            _task = task;
            _propertyName = propertyName;
            _delimiter = delimiter.IsNullOrEmpty() ? ";" : delimiter;
        }

        public void Record(string filePath)
        {
            _filePath = filePath;
            
            if (_recordedCount > 0)
                _builder.Append(_delimiter);

            _builder.Append(string.Concat("\"", filePath, "\""));

            _recordedCount++;
        }

        public void Log(string message)
        {
            _task.Log(Level.Debug, message);
        }

        public void Dispose()
        {
            if (_recordedCount > 0)
            {
                var delimited = _builder.ToString();

                if (!_propertyName.IsNullOrEmpty())
                    _task.Properties[_propertyName] = delimited;

                _task.Log(Level.Debug, "Recording (Count: {0}): {1}".FormatWith(_recordedCount, delimited));
            }

            GC.SuppressFinalize(this);
        }
    }
}