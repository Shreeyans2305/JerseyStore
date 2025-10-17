using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace JerseyStore
{
    public class CircleButton : Button
    {
        public CircleButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.BackColor = Color.DodgerBlue;
            this.ForeColor = Color.White;
            this.Size = new Size(60, 60);
            this.Text = "";
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            // Keep the button square
            int min = Math.Min(this.Width, this.Height);
            this.Width = this.Height = min;
            using (var path = new GraphicsPath())
            {
                path.AddEllipse(0, 0, min, min);
                this.Region = new Region(path);
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw circle background
            using (Brush b = new SolidBrush(this.BackColor))
            {
                g.FillEllipse(b, 0, 0, this.Width - 1, this.Height - 1);
            }

            // Draw border
            using (Pen p = new Pen(this.ForeColor, 2))
            {
                g.DrawEllipse(p, 1, 1, this.Width - 3, this.Height - 3);
            }

            // Draw text centered
            TextRenderer.DrawText(
                g,
                this.Text,
                this.Font,
                new Rectangle(0, 0, this.Width, this.Height),
                this.ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );
        }
    }
}