using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using NPlant.Console.Exceptions;
using NPlant.Generation;
using NPlant.Generation.ClassDiagraming;
using Con=System.Console;

namespace NPlant.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var arguments = new CommandLineArgs(args);

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
                        Con.WriteLine($"    {matchingDiagram.Diagram.Name}");                        
                    }
                    else
                    {
                        string diagramText = BufferedClassDiagramGenerator.GetDiagramText(matchingDiagram.Diagram);
                        ImageFileGenerationModel model = ImageFileGenerationModel.Create(diagramText, matchingDiagram.Diagram.Name, arguments.Java);

                        DirectoryInfo outputDirectory = new DirectoryInfo(arguments.Output);

                        if (!outputDirectory.Exists)
                            outputDirectory.Create();

                        string path = Path.Combine(outputDirectory.FullName, $"{model.DiagramName}.{arguments.Format}");
                        ImageFormat format = arguments.GetImageFormat();

                        if (format == null)
                        {
                            File.AppendAllText(path, diagramText);
                        }
                        else
                        {
                            NPlantImage nplantImage = new NPlantImage(model.JavaPath, model.Invocation);
                            Image image = nplantImage.Create(model.DiagramText, model.DiagramName);
                            image.Save(path, format);
                        }
                    }


                }

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
                Con.WriteLine(consoleException.Message);
            }
        }
    }

    public class ImageFileGenerationModel
    {

        private ImageFileGenerationModel(string diagramText, string diagramName, string javaPath)
        {
            if(javaPath.IsNullOrEmpty())
                javaPath = SystemEnvironment.GetSettings().JavaPath;

            this.JavaPath = javaPath;
            this.DiagramText = diagramText;
            this.DiagramName = diagramName;
        }

        public static ImageFileGenerationModel Create(string diagramText, string diagramName)
        {
            return new ImageFileGenerationModel(diagramText, diagramName, null);
        }

        public static ImageFileGenerationModel Create(string diagramText, string diagramName, string javaPath)
        {
            return new ImageFileGenerationModel(diagramText, diagramName, javaPath);
        }

        public string JavaPath { get; private set; }
        public string DiagramText { get; private set; }
        public string DiagramName { get; private set; }

        public PlantUmlInvocation Invocation { get; } = new PlantUmlInvocation(SystemEnvironment.ExecutionDirectory);
    }

    public static class SystemEnvironment
    {
        public static string ExecutionDirectory => Environment.CurrentDirectory;

        public static SystemSettings GetSettings()
        {
            string javaHome = Environment.GetEnvironmentVariable("NPLANT_JAVA_HOME", EnvironmentVariableTarget.User);

            if (javaHome.IsNullOrEmpty())
                javaHome = System.Environment.GetEnvironmentVariable("JAVA_HOME", EnvironmentVariableTarget.User);

            return new SystemSettings()
            {
                JavaPath = javaHome
            };
        }

        public static void SetSettings(SystemSettings settings)
        {
            Environment.SetEnvironmentVariable("NPLANT_JAVA_HOME", settings.JavaPath, EnvironmentVariableTarget.User);
        }
    }

    public class SystemSettings
    {
        public string JavaPath { get; set; }
    }

}
