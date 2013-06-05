using System;
using System.Windows.Forms;

namespace NPlant.UI
{
    public static class ScreenManager
    {
        public static void Launch<TScreen>(IWin32Window parent, Action<TScreen> onOK = null, Action<TScreen> onCancel = null) where TScreen : IScreen, new()
        {
            var t = new TScreen();
            var dialogResult = t.ShowDialog(parent);

            if (onOK != null && IsAffirmative(dialogResult))
            {
                onOK(t);
            }
            else if (onCancel != null && IsNegative(dialogResult))
            {
                onCancel(t);
            }
        }

        public static TResult Launch<TScreen, TResult>(IWin32Window parent, Action<TScreen, TResult> onOK = null, Action<TScreen, TResult> onCancel = null) where TScreen : IResultScreen<TResult>, new()
        {
            var t = new TScreen();
            var dialogResult = t.ShowDialog(parent);
            var result = t.GetResult();

            if (onOK != null && IsAffirmative(dialogResult))
            {
                onOK(t, result);
            }
            else if (onCancel != null && IsNegative(dialogResult))
            {
                onCancel(t, result);
            }

            return result;
        }

        private static bool IsAffirmative(DialogResult dialogResult)
        {
            return (dialogResult == DialogResult.OK || dialogResult == DialogResult.Yes);
        }

        private static bool IsNegative(DialogResult dialogResult)
        {
            return (dialogResult == DialogResult.Cancel || dialogResult == DialogResult.No);
        }
    }
}