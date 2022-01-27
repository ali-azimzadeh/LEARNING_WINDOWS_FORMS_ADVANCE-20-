using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LEARNING_WINDOWS_FORMS_ADVANCE_20_
{
    public class ProgressBar : System.Windows.Forms.ProgressBar
    {
        public ProgressBar() : base()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            const int inset = 2; // A single inset value to control teh sizing of the inner rect.

            using (Image offscreenImage = new Bitmap(this.Width, this.Height))
            {
                using (Graphics offscreen = Graphics.FromImage(offscreenImage))
                {
                    Rectangle rect = 
                        new Rectangle(0, 0, this.Width, this.Height);

                    if (ProgressBarRenderer.IsSupported)
                    {
                        ProgressBarRenderer.DrawHorizontalBar(offscreen, rect);
                    }

                    rect.Inflate(new Size(-inset, -inset)); // Deflate inner rect.

                    //  rect.Width = rect.Width * (this.Maximum / this.Value);
                    rect.Width = 
                        (this.Value * (rect.Width / this.Maximum));

                    if (rect.Width == 0)
                    {
                        rect.Width = 1; // Can't draw rec with width of 0.
                    }

                    LinearGradientBrush brush = 
                        new LinearGradientBrush(rect, this.BackColor, this.ForeColor, LinearGradientMode.Vertical);

                    offscreen.FillRectangle(brush, inset, inset, rect.Width, rect.Height);

                    e.Graphics.DrawImage(offscreenImage, 0, 0);

                    offscreenImage.Dispose();
                }
            }

            base.OnPaint(e);
        }
    }
}
