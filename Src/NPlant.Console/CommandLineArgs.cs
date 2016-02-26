using System.ComponentModel;
using System.Drawing.Imaging;

namespace NPlant.Console
{
    public class CommandLineArgs
    {
        private string _format;
        private ImageFormat _imageFormat;

        public CommandLineArgs(string[] args)
        {
            this.Format = "png";

            CommandLineMapper.Map(this, args);
        }

        [RequiredArgument]
        public string Assembly { get; protected set; }

        public string Diagram { get; protected set; }

        public string Java { get; protected set; }

        public string Output { get; protected set; }

        public string Format
        {
            get { return _format; }
            protected set
            {
                _format = value;

                if (!string.Equals("nplant", _format))
                {
                    var converter = TypeDescriptor.GetConverter(typeof(ImageFormat));
                    _imageFormat = (ImageFormat)converter.ConvertFromInvariantString(_format);
                }
            }
        }

        public bool Debugger { get; protected set; }

        public ImageFormat GetImageFormat()
        {
            return _imageFormat;
        }
    }
}