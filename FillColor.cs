using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BTLON_paint
{
    internal class FillColor
    {
        private void set_color(Bitmap b, Stack<Point> sp, int x, int y, Color old_c, Color new_c)
        {
            Color c = b.GetPixel(x, y);
            if (c == old_c)
            {
                sp.Push(new Point(x, y));
                b.SetPixel(x, y, new_c);
            }
        }

        public void Fill(PictureBox pb, Bitmap b, int x, int y, Color new_c)
        {
            Color old_c = b.GetPixel(x, y);
            Stack<Point> sp = new Stack<Point>();
            sp.Push(new Point(x, y));
            b.SetPixel(x, y, new_c);
            if (old_c.ToArgb() == new_c.ToArgb()) return;
            while (sp.Count > 0)
            {
                Point p = (Point)sp.Pop();
                if (p.X > 0 && p.X < pb.Width - 1 && p.Y > 0 && p.Y < pb.Height - 1)
                {
                    set_color(b, sp, p.X + 1, p.Y, old_c, new_c);
                    set_color(b, sp, p.X, p.Y + 1, old_c, new_c);
                    set_color(b, sp, p.X - 1, p.Y, old_c, new_c);
                    set_color(b, sp, p.X, p.Y - 1, old_c, new_c);
                }
            }
        }
    }
}