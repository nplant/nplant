using System;
using System.IO;

namespace NPlant.UI.Screens.Controls
{
    public class ImageFileWatcher
    {
        private readonly string _path;
        private readonly Action _callback;

        public ImageFileWatcher(string path, Action callback)
        {
            _path = path.CheckForNullOrEmptyArg("path");
            _callback = callback.CheckForNullArg("callback");
        }

        public void Watch()
        {
            string directory = Path.GetDirectoryName(_path);
            string file = Path.GetFileName(_path);

            var watcher = new FileSystemWatcher
            {
                Path = directory,
                Filter = file
            };

            watcher.Created += (sender, args) => _callback();

            watcher.EnableRaisingEvents = true;
        }
    }
}