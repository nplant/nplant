using System.Reflection;
using NPlant.MetaModel.ClassDiagraming;

namespace NPlant.Generation.ClassDiagraming
{
    public class ClassDiagramOptions
    {
        private readonly ClassDiagram _diagram;

        public ClassDiagramOptions(ClassDiagram diagram)
        {
            _diagram = diagram;
        }

        public ForTypeDescriptor<T> ForType<T>()
        {
            return new ForTypeDescriptor<T>(_diagram);
        }

        public ClassDiagramOptions ScanModeOf(ClassDiagramScanModes scanMode)
        {
            _diagram.ScanMode = scanMode;
            
            return this;
        }

        public ClassDiagramOptions ShowMethods()
        {
            _diagram.ShowMethods = true;
            _diagram.ShowMethodsBindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

            return this;
        }

        public ClassDiagramOptions ShowMethods(BindingFlags flags)
        {
            _diagram.ShowMethods = true;
            _diagram.ShowMethodsBindingFlags = flags;

            return this;
        }

        public ClassDiagramOptions FanningDepthLimit(int depth)
        {
            _diagram.DepthLimit = depth;

            return this;
        }
    }

    public enum ClassDiagramScanModes
    {
        AllMembers,
        PublicMembersOnly,
        SystemServiceModelMember
    }
}