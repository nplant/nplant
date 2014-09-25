using System.Reflection;
using NPlant.MetaModel.ClassDiagraming;

namespace NPlant.Generation.ClassDiagraming
{
    public class ClassDiagramOptions
    {
        public static BindingFlags ShowMembersBindingFlagsDefault = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        public static BindingFlags ShowMethodsBindingFlagsDefault = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly;

        private readonly ClassDiagram _diagram;

        public ClassDiagramOptions(ClassDiagram diagram)
        {
            _diagram = diagram;
            this.ShowMembers();
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
            _diagram.ShowMethodsBindingFlags = ShowMethodsBindingFlagsDefault;

            return this;
        }

        public ClassDiagramOptions ShowMethods(BindingFlags flags)
        {
            _diagram.ShowMethods = true;
            _diagram.ShowMethodsBindingFlags = flags;

            return this;
        }

        public ClassDiagramOptions HideMethods()
        {
            _diagram.ShowMethods = false;
            return this;
        }
        public ClassDiagramOptions ShowMembers()
        {
            _diagram.ShowMembers = true;
            _diagram.ShowMembersBindingFlags = ShowMembersBindingFlagsDefault;

            return this;
        }

        public ClassDiagramOptions ShowMembers(BindingFlags flags)
        {
            _diagram.ShowMembers = true;
            _diagram.ShowMembersBindingFlags = flags;

            return this;
        }

        public ClassDiagramOptions HideMembers()
        {
            _diagram.ShowMembers = false;
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