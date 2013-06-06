using System;
using System.IO;
using System.Reflection;
using NPlant.Core;

namespace NPlant.Generation
{
    public class NPlantAssemblyLoader
    {
        private readonly IRunnerRecorder _recorder = NullRecorder.Instance;

        public NPlantAssemblyLoader() { }

        public NPlantAssemblyLoader(IRunnerRecorder recorder)
        {
            _recorder = recorder;
        }

        public Assembly Load(string path)
        {
            _recorder.Log("Starting Stage: Assembly Load (assembly={0})...".FormatWith(path));

            path.CheckForNull(() => new NPlantException("An 'assembly' attribute is required."));

            string loadMessage;

            Assembly assembly = LoadAssembly(path, out loadMessage);

            assembly.CheckForNull(
                () =>
                new NPlantException(
                    "Failed to load assembly '{0}'.  Exception message detected:  {1}".FormatWith(path, loadMessage)));

            _recorder.Log("Finished Stage: Assembly Load...");
            
            return assembly;
        }

        private Assembly LoadAssembly(string path, out string message)
        {
            Assembly assembly;

            try
            {
                if (Path.IsPathRooted(path))
                    assembly = Assembly.LoadFrom(path);
                else
                    assembly = Assembly.Load(path);
            }
            catch (Exception exception)
            {
                message = exception.Message;
                return null;
            }

            message = null;

            return assembly;
        }
    }
}