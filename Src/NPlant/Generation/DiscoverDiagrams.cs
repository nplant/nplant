using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NPlant.Core;

namespace NPlant.Generation
{
    public static class DiscoverDiagrams
    {
        static readonly Type DiagramInterface = typeof(IDiagram);
        static readonly Type DiagramFactoryInterface = typeof(IDiagramFactory);

        public static IDiagram[] InAssembly(Assembly assembly, Action<string> logger = null)
        {
            if (logger == null)
                logger = (msg) => { };

            if (assembly == null)
            {
                logger("Assembly was null");

                return new IDiagram[0];
            }

            var diagrams = new List<IDiagram>();

            var exportedTypes = assembly.GetExportedTypes().Where(x => ! x.HasAttribute<HideDiagramAttribute>()).ToArray();

            logger("ExportedTypes Count from '{0}':  {1}".FormatWith(assembly.FullName, exportedTypes.Length));

            foreach (var exportedType in exportedTypes)
            {
                if (! exportedType.IsAbstract)
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

                        diagrams.AddRange(factory.InternalGetDiagrams().Cast<IDiagram>());
                    }
                }
            }

            return diagrams.ToArray();
        }

        private static IDiagramFactory InstantiateDiagramFactory(Type exportedType)
        {
            ConstructorInfo ctor;

            if (!exportedType.TryGetPublicParameterlessConstructor(out ctor))
                throw new NPlantException("DiagramFactory's are expected to have a public parameterless constructor.  '{0}' does not meet this expectation.".FormatWith(exportedType.FullName));

            return (IDiagramFactory)ctor.Invoke(new object[0]);
        }

        private static IDiagram InstantiateDiagram(Type exportedType)
        {
            ConstructorInfo ctor;

            if (!exportedType.TryGetPublicParameterlessConstructor(out ctor))
                throw new NPlantException("Diagrams are expected to have a public parameterless constructor.  '{0}' does not meet this expectation.".FormatWith(exportedType.FullName));

            return (IDiagram)ctor.Invoke(new object[0]);
        }
    }
}