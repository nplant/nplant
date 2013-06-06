using System;
using System.Windows.Forms;

namespace NPlant.UI
{
    public class WaitCursor : IDisposable
    {
        private readonly Cursor _old;

        public WaitCursor()
        {
            _old = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
        }

        public void Dispose()
        {
            Cursor.Current = _old;
        }
    }
}
