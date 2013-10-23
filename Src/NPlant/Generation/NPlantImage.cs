using System;
using System.Diagnostics;
using System.Drawing;
using NPlant.Core;

namespace NPlant.Generation
{
    public class NPlantImage
    {
        private readonly string _javaPath;
        private readonly PlantUmlInvocation _invocation;
        private Action<string> _logger = text => Console.WriteLine(text);

        public NPlantImage(string javaPath, PlantUmlInvocation invocation)
        {
            _javaPath = javaPath;
            _invocation = invocation;
        }

        public Action<string> Logger
        {
            get { return _logger; }
            set
            {
                if(value != null)
                    _logger = value;
            }
        }

        public Image Create(string diagramText)
        {
            try
            {
                Process process = new Process
                    {
                        StartInfo = 
                            {
                                FileName = _javaPath,
                                Arguments = _invocation.ToString(),
                                UseShellExecute = false,
                                CreateNoWindow = true,
                                RedirectStandardError = true,
                                RedirectStandardInput = true,
                                RedirectStandardOutput = true
                            },
                        EnableRaisingEvents = true
                    };

                Logger("Invoking plantuml - FileName: {0}, Arguments: {1}".FormatWith(process.StartInfo.FileName, process.StartInfo.Arguments));

                bool started = process.Start();

                if (started)
                {
                    process.StandardInput.Write(diagramText);
                    process.StandardInput.Close();

                    return Image.FromStream(process.StandardOutput.BaseStream);
                }
                
                Logger("Failed to start plantuml");

                return null;
            }
            catch (Exception ex)
            {
                Logger("Unhandled exception occurred while invoking plantuml: " + ex);

                if (ex.IsDontMessWithMeException())
                    throw;

                throw new NPlantException("Image generation failed - {0}.  Check the inner exception for details.".FormatWith(ex.Message), ex);
            }
        }
    }
}