using System;
using System.Drawing;
using System.Windows.Forms;

namespace NPlant.UI.Screens.Controls
{
    public class SmartPictureBox : PictureBox
    {
        private int _zoomLevel = 1;
        private Point _zoomPoint;

        public SmartPictureBox()
        {
            this.MouseWheel += OnMouseWheel;
        }

        void OnMouseWheel(object sender, MouseEventArgs e)
        {
            if (this._zoomLevel == 1)
            {
                this._zoomPoint = new Point(e.X, e.Y);
            }
            if (e.Delta > 0)
            {
                if (_zoomLevel < 20)
                {
                    _zoomLevel += 1;
                }
            }
            else
            {
                if (e.Delta < 1)
                {
                    if (_zoomLevel > 1)
                    {
                        _zoomLevel -= 1;
                    }
                }
            }

            this.Invalidate();
        }

        public void ClearImage()
        {
            SetImage(null);
        }

        public void SetImage(Image image)
        {            
            _zoomLevel = 1;
            base.Image = image;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            //base.OnPaint(pe);
            if (this.Image != null)
            {
                Point location;
                Size sz;

                if (_zoomLevel != 1)
                {
                    sz = new Size(this.Image.Width / _zoomLevel, this.Image.Height / _zoomLevel);
                    // center on _zoomPoint. Casts are needed so integer divide doesn't occur (intermediate double result)
                    location = new Point((int)(this.Image.Width * (_zoomPoint.X / (double)this.ClientRectangle.Width)) - sz.Width / 2,
                                     (int)(this.Image.Height * (_zoomPoint.Y / (double)this.ClientRectangle.Height)) - sz.Height / 2);
                }
                else
                {
                    location = new Point(0, 0);
                    sz = this.Image.Size;
                }
                Rectangle rectSrc = new Rectangle(location, sz);
                // now draw the rect of the source picture in the entire client rect of MyPictureBox
                pe.Graphics.DrawImage(this.Image, this.ClientRectangle, rectSrc, GraphicsUnit.Pixel);
            }
        }
    }
}
