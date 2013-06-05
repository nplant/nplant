using System.Windows.Forms;

namespace NPlant.UI
{
    public interface IScreen
    {
        DialogResult ShowDialog(IWin32Window owner);
    }
}