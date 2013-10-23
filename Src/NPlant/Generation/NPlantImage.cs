using System;
using System.Diagnostics;
using System.Drawing;
using NPlant.Core;

namespace NPlant.Generation
{
    public static class NPlantImage
    {

        public static Image Create(string diagramText, string javaPath, string javaArgs)
        {

            try
            {
                Process process = new Process
                    {
                        StartInfo = 
                            {
                                FileName = javaPath,
                                Arguments = javaArgs,
                                UseShellExecute = false,
                                CreateNoWindow = true,
                                RedirectStandardError = true,
                                RedirectStandardInput = true,
                                RedirectStandardOutput = true
                            },
                        EnableRaisingEvents = true
                    };

                process.Start();

                process.StandardInput.Write(diagramText);
                process.StandardInput.Close();

                return Image.FromStream(process.StandardOutput.BaseStream);
            }
            catch (Exception ex)
            {
                if (ex.IsDontMessWithMeException())
                    throw;

                Console.Write(ex);

                throw new NPlantException("Image generation failed - {0}.  Check the inner exception for details.".FormatWith(ex.Message), ex);
            }
        }
    }
}