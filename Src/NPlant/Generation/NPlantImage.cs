using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
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
            _javaPath = javaPath ?? "java.exe";
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

        public Image Create(string diagramText, string diagramName)
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

                Logger("Invoking plantuml - Diagram: {0}, FileName: {1}, Arguments: {2}".FormatWith(diagramName, process.StartInfo.FileName, process.StartInfo.Arguments));

                bool started = process.Start();

                if (started)
                {
                    process.StandardInput.Write(diagramText);
                    process.StandardInput.Close();

                    if (process.StandardOutput.BaseStream == null)
                        throw new NPlantException("While invoking plant uml, the standard output was empty. Error out: {0}".FormatWith(process.StandardError.ReadToEnd()));

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

                string message = CreateException(ex);

                throw new NPlantException(message, ex);
            }
        }

        private string CreateException(Exception exception)
        {
            Win32Exception win32 = exception as Win32Exception;

            if (win32 != null)
            {
                if (! File.Exists(_javaPath))
                {
                    if (Path.IsPathRooted(_javaPath))
                    {
                        return "It appears the path to your local JRE installation is specified incorrectly in Options -> Settings.  '{0}' could not be found.".FormatWith(_javaPath);
                    }

                    return "It appears the exact location of your JRE installation is not specified in your Options -> Settings.  This tool assumes java.exe is in your system PATH if not otherwise specified in your settings.  Either add java.exe to your path, or explicitly specify where we can find java.exe.  If you don't have the Java JRE installed, use the Help menu to go get it.";
                }
            }

            StringBuilder buffer = new StringBuilder();
            buffer.AppendLine("Failed to invoke plant uml.  See the inner exception for more details.  Messages:");
                
            Exception ex = exception;

            while (ex != null)
            {
                buffer.AppendLine(ex.Message);
                ex = ex.InnerException;
            }

            return buffer.ToString();
        }
    }
}