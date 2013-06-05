using System.IO;
using NPlant.Core;

namespace NPlant.UI
{
    public class CommandLineArguments
    {
        protected CommandLineArguments(string[] args)
        {
            if (args == null || args.Length < 1)
                return;

            var path = args[0].CheckForNull();

            ValidateFilePath(path);

            this.FilePath = path;
        }

        private void ValidateFilePath(string path)
        {
            if (path.IsAssemblyFilePath())
                return;

            if (path.IsNPlantFilePath())
                return;

            string extension = Path.GetExtension(path);

            throw new NPlantException("'{0}' is not a recognized/supported file extension.  This version of NPlant.UI.exe supports .nplant text files or .NET assembly files (.dll/.exe)".FormatWith(extension));
        }

        public string FilePath { get; protected set; }

        public bool HasFilePath
        {
            get { return ! this.FilePath.IsNullOrEmpty(); }
        }

        public static CommandLineArguments Load(string[] args)
        {
            return new CommandLineArguments(args);
        }
    }
}
