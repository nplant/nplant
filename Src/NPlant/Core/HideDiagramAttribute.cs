using System;

namespace NPlant.Core
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class HideDiagramAttribute : Attribute
    {
    }
}