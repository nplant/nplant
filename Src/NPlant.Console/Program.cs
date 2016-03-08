using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using NPlant.Console.Exceptions;
using NPlant.Core;
using NPlant.Generation;
using NPlant.Generation.ClassDiagraming;
using Con=System.Console;

namespace NPlant.Console
{
    class Program
    {
        static int Main(string[] args)
        {

            try
            {
                var arguments = new CommandLineArgs(args);

                string jarPath = arguments.Jar;

                if (jarPath.IsNullOrEmpty())
                    jarPath = PlantUmlJarExtractor.TryExtractTo(ConsoleEnvironment.ExecutionDirectory);

                if (arguments.Debugger)
                {
                    Debugger.Launch();
                    Debugger.Break();
                }

                var assemblyLoader = new NPlantAssemblyLoader();
                Assembly assembly = assemblyLoader.Load(arguments.Assembly);

                var diagramLoader = new NPlantDiagramLoader();

                var diagrams = diagramLoader.Load(assembly);

                IEnumerable<DiscoveredDiagram> matchingDiagrams = diagrams;

                if(! arguments.Diagram.IsNullOrEmpty())
                    matchingDiagrams = matchingDiagrams.Where(diagram => diagram.Diagram.Name.StartsWith(arguments.Diagram));

                foreach (var matchingDiagram in matchingDiagrams)
                {
                    if (string.IsNullOrEmpty(arguments.Output))
                    {
                        Con.WriteLine("    {0}", matchingDiagram.Diagram.Name);                        
                    }
                    else
                    {
                        string diagramText = BufferedClassDiagramGenerator.GetDiagramText(matchingDiagram.Diagram);
                        ImageFileGenerationModel model = new ImageFileGenerationModel(diagramText, matchingDiagram.Diagram.Name, arguments.Java, jarPath);

                        DirectoryInfo outputDirectory = new DirectoryInfo(arguments.Output);

                        if (!outputDirectory.Exists)
                            outputDirectory.Create();

                        string path = Path.Combine(outputDirectory.FullName, string.Format("{0}.{1}" ,model.DiagramName, arguments.Format));
                        ImageFormat format = arguments.GetImageFormat();

                        if (format == null)
                        {
                            File.WriteAllText(path, diagramText);
                        }
                        else
                        {
                            NPlantImage nplantImage = new NPlantImage(model.JavaPath, model.Invocation);
                            Image image = nplantImage.Create(model.DiagramText, model.DiagramName);
                            image.Save(path, format);
                        }
                    }
                }

                return 0;
            }
            catch (ConsoleUsageException usageException)
            {
                Con.WriteLine("Fatal Error:");
                Con.WriteLine(usageException.Message);
                Con.WriteLine();
                Con.WriteLine("NPlant.Console.exe Usage");
                Con.WriteLine("------------------------");
            }
            catch (Exception consoleException)
            {
                Con.WriteLine("Fatal Error:");
                Con.WriteLine(consoleException);
            }

            return 1;
        }
    }
}
