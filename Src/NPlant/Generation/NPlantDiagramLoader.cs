using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NPlant.Core;

namespace NPlant.Generation
{
    public class NPlantDiagramLoader
    {
        static readonly Type DiagramInterface = typeof(IDiagram);
        static readonly Type DiagramFactoryInterface = typeof(IDiagramFactory);

        private readonly IRunnerRecorder _recorder = NullRecorder.Instance;

        public NPlantDiagramLoader(){}

        public NPlantDiagramLoader(IRunnerRecorder recorder)
        {
            _recorder = recorder;
        }

        public IEnumerable<DiscoveredDiagram> Load(Assembly assembly)
        {
            return Load(assembly, type => true);
        }

        public IEnumerable<DiscoveredDiagram> Load(Assembly assembly, Func<Type, bool> matcher)
        {
            _recorder.Log("Starting Stage: Diagram Instantiation...");

            DiscoveredDiagram[] diagrams = LoadFromAssembly(assembly, _recorder.Log);

            _recorder.Log("Finished Stage: Diagram Instantiation (diagrams instantiated={0})...".FormatWith(diagrams.Length));

            return diagrams;
        }

        private static DiscoveredDiagram[] LoadFromAssembly(Assembly assembly, Action<string> logger = null)
        {
            if (logger == null)
                logger = (msg) => { };

            if (assembly == null)
            {
                logger("Assembly was null");

                return new DiscoveredDiagram[0];
            }

            var diagrams = new List<DiscoveredDiagram>();

            var exportedTypes = assembly.GetExportedTypes().Where(x => !x.HasAttribute<HideDiagramAttribute>()).ToArray();

            logger("ExportedTypes Count from '{0}':  {1}".FormatWith(assembly.FullName, exportedTypes.Length));

            foreach (var exportedType in exportedTypes)
            {
                if (!exportedType.IsAbstract)
                {
                    if (DiagramInterface.IsAssignableFrom(exportedType))
                    {
                        logger("ExportedType '{0}' found to be assignable to {1}.".FormatWith(exportedType.FullName, DiagramInterface.FullName));

                        diagrams.Add(InstantiateDiagram(exportedType));
                    }
                    else if (DiagramFactoryInterface.IsAssignableFrom(exportedType))
                    {
                        logger("ExportedType '{0}' found to be assignable to {1}.".FormatWith(exportedType.FullName, DiagramFactoryInterface.FullName));

                        var factory = InstantiateDiagramFactory(exportedType);

                        diagrams.AddRange(factory.GetDiagrams().Select(diagram => new DiscoveredDiagram(exportedType.Namespace, diagram)));
                    }
                }
            }

            return diagrams.ToArray();
        }

        private static IDiagramFactory InstantiateDiagramFactory(Type exportedType)
        {
            ConstructorInfo ctor;

            if (!exportedType.TryGetPublicParameterlessConstructor(out ctor))
                throw new NPlantException("ClassDiagramFactory's are expected to have a public parameterless constructor.  '{0}' does not meet this expectation.".FormatWith(exportedType.FullName));

            return (IDiagramFactory)ctor.Invoke(new object[0]);
        }

        private static DiscoveredDiagram InstantiateDiagram(Type exportedType)
        {
            ConstructorInfo ctor;

            if (!exportedType.TryGetPublicParameterlessConstructor(out ctor))
                throw new NPlantException("Diagrams are expected to have a public parameterless constructor.  '{0}' does not meet this expectation.".FormatWith(exportedType.FullName));

            return new DiscoveredDiagram(exportedType.Namespace, (IDiagram)ctor.Invoke(new object[0]));
        }

    }
}