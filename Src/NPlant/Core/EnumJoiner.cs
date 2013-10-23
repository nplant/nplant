using System;

namespace NPlant.Core
{
    public static class EnumJoiner
    {
        public static string Join<T>()
        {
            var names = Enum.GetNames(typeof (T));
            return string.Join(",", names);
        }
    }
}
