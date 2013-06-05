namespace NPlant.UI
{
    public class CommandLineArguments
    {
        protected CommandLineArguments(string[] args)
        {
            if (args == null || args.Length < 1)
                return;

            this.FilePath = args[0].CheckForNull();
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
