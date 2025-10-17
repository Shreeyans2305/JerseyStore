using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace JerseyStore
{
public class RoundedButton : Button
{
    public int BorderRadius { get; set; } = 20;
    public Color BorderColor { get; set; } = Color.Black;
    public int BorderSize { get; set; } = 2;

    private Color _hoverBackColor;
    private Color _originalBackColor;

    public RoundedButton()
    {
        FlatStyle = FlatStyle.Flat;
        FlatAppearance.BorderSize = 0;
        BackColor = Color.MediumSlateBlue;
        ForeColor = Color.White;
        _originalBackColor = BackColor;
        _hoverBackColor = Color.OrangeRed;
        DoubleBuffered = true;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        Rectangle rectSurface = this.ClientRectangle;
        Rectangle rectBorder = Rectangle.Inflate(rectSurface, -BorderSize, -BorderSize);
        int radius = BorderRadius;

        using (GraphicsPath pathSurface = GetRoundedPath(rectSurface, radius))
        using (GraphicsPath pathBorder = GetRoundedPath(rectBorder, radius - BorderSize))
        using (Pen penSurface = new Pen(this.Parent.BackColor, 2))
        using (Pen penBorder = new Pen(BorderColor, BorderSize))
        {
            this.Region = new Region(pathSurface);
            e.Graphics.DrawPath(penSurface, pathSurface);

            // Draw button background
            using (SolidBrush brush = new SolidBrush(this.BackColor))
                e.Graphics.FillPath(brush, pathSurface);

            // Draw border
            if (BorderSize >= 1)
                e.Graphics.DrawPath(penBorder, pathBorder);
        }

        TextRenderer.DrawText(e.Graphics, Text, Font, rectSurface, ForeColor,
            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
    }

    private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
    {
        GraphicsPath path = new GraphicsPath();
        float curveSize = radius * 2F;

        path.StartFigure();
        path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
        path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
        path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
        path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
        path.CloseFigure();
        return path;
    }

    protected override void OnMouseEnter(EventArgs e)
    {
        base.OnMouseEnter(e);
        _originalBackColor = BackColor;
        BackColor = _hoverBackColor;
        Invalidate();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        BackColor = _originalBackColor;
        Invalidate();
    }

    public void SetHoverColor(Color color)
    {
        _hoverBackColor = color;
    }
}
}
