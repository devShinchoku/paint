using System.Drawing;

namespace BTLON_paint
{
    internal class Diamond
    {
        public void DrawDiamond(Graphics g, Pen p, int x, int y, int w, int h)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddLine(x + w / 2, y, x + w, y + h / 2);
            path.AddLine(x + w, y + h / 2, x + w / 2, y + h);
            path.AddLine(x + w / 2, y + h, x, y + h / 2);
            path.AddLine(x, y + h / 2, x + w / 2, y);
            path.CloseFigure();
            g.DrawPath(p, path);
        }

        public void FillDiamond(Graphics g, Pen p, int x, int y, int w, int h)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddLine(x + w / 2, y, x + w, y + h / 2);
            path.AddLine(x + w, y + h / 2, x + w / 2, y + h);
            path.AddLine(x + w / 2, y + h, x, y + h / 2);
            path.AddLine(x, y + h / 2, x + w / 2, y);
            path.CloseFigure();
            g.FillPath(p.Brush, path);
        }
    }
}