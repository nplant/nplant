using System.IO;
using System.Windows.Forms;

namespace NPlant.UI
{
    public static class SystemEnvironment
    {
        public static string ExecutionDirectory
        {
            get { return Path.GetDirectoryName(Application.ExecutablePath); }
        }
    }
}
