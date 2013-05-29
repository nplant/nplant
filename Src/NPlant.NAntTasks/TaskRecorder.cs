using System;
using System.Text;

namespace NPlant.NAntTasks
{
    public class TaskRecorder : IDisposable
    {
        private readonly string _delimiter;
        private readonly StringBuilder _builder = new StringBuilder();
        private int _recordedCount;
        private Action<string> _action = (recording) => {};

        public TaskRecorder(string delimiter)
        {
            _delimiter = delimiter.IsNullOrEmpty() ? ";" : delimiter;
        }

        public void Record(string filePath)
        {
            if (_recordedCount > 0)
                _builder.Append(_delimiter);

            _builder.Append(string.Concat("\"", filePath, "\""));

            _recordedCount++;
        }

        public void OnDispose(Action<string> action)
        {
            _action = action;
        }

        public void Dispose()
        {
            if (_recordedCount > 0)
                _action(_builder.ToString());

            GC.SuppressFinalize(this);
        }
    }
}