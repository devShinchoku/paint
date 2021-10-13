using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace BTLON_paint
{
    public partial class Paint : Form
    {
        #region variables

        private Bitmap bm;
        private bool mdown = false, drawed = false, isdone = false;
        private Point lp = Point.Empty;
        private List<Point> listp = null;
        private Graphics g;
        private Pen pen;
        private Pen eraser = new Pen(Color.White, 5);
        private int index = 1, sX, sY, cX, cY, w, h, index_style = 0;
        private Color c1 = Color.Black, c2 = Color.White;

        #endregion variables

        #region Form

        public Paint()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btn_pen.Focus();
            g = pb_paint.CreateGraphics();
            pen = new Pen(c1, 3);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            bm = new Bitmap(pb_paint.Width, pb_paint.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            pb_paint.Image = bm;
            lbl_sizedraw.Text = pb_paint.Width + ", " + pb_paint.Height;
            drawed = false;
        }

        #endregion Form

        #region Paint

        #region Mouse Down

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                pen.Color = c1;
            if (e.Button == MouseButtons.Right)
                pen.Color = c2;
            mdown = true;
            lp = e.Location;
            sX = e.X; sY = e.Y;
            if ((index == 7 || index == 8) && listp == null)
            {
                listp = new List<Point>();
                listp.Add(e.Location);
            }
        }

        #endregion Mouse Down

        #region Mouse Up

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            mdown = false;
            drawed = true;
            w = Math.Max(cX, sX) - Math.Min(cX, sX);
            h = Math.Max(cY, sY) - Math.Min(cY, sY);
            if (index == 3)
            {
                if (index == 3)
                {
                    if (index_style == 0)
                        g.DrawRectangle(pen, Math.Min(sX, cX), Math.Min(sY, cY), w, h);
                    if (index_style == 1)
                        g.FillRectangle(pen.Brush, Math.Min(sX, cX), Math.Min(sY, cY), w, h);
                    if (index_style == 2)
                    {
                        Pen p2 = new Pen(c2);
                        if (c2 == pen.Color)
                            p2.Color = c1;
                        g.FillRectangle(p2.Brush, Math.Min(sX, cX), Math.Min(sY, cY), w, h);
                        g.DrawRectangle(pen, Math.Min(sX, cX), Math.Min(sY, cY), w, h);
                    }
                }
            }
            if (index == 4)
            {
                if (index_style == 0)
                    g.DrawEllipse(pen, Math.Min(sX, cX), Math.Min(sY, cY), w, h);
                if (index_style == 1)
                    g.FillEllipse(pen.Brush, Math.Min(sX, cX), Math.Min(sY, cY), w, h);
                if (index_style == 2)
                {
                    Pen p2 = new Pen(c2);
                    if (c2 == pen.Color)
                        p2.Color = c1;
                    g.FillEllipse(p2.Brush, Math.Min(sX, cX), Math.Min(sY, cY), w, h);
                    g.DrawEllipse(pen, Math.Min(sX, cX), Math.Min(sY, cY), w, h);
                }
            }
            if (index == 5)
            {
                g.DrawLine(pen, sX, sY, cX, cY);
            }
            if (index == 6)
            {
                RoundedRetangle rr = new RoundedRetangle();
                if (index_style == 0)
                    rr.DrawRoundedRectangle(g, pen, Math.Min(sX, cX), Math.Min(sY, cY), w, h, 4);
                if (index_style == 1)
                    rr.FillRoundedRectangle(g, pen, Math.Min(sX, cX), Math.Min(sY, cY), w, h, 4);
                if (index_style == 2)
                {
                    Pen p2 = new Pen(c2);
                    if (c2 == pen.Color)
                        p2.Color = c1;
                    rr.FillRoundedRectangle(g, p2, Math.Min(sX, cX), Math.Min(sY, cY), w, h, 4);
                    rr.DrawRoundedRectangle(g, pen, Math.Min(sX, cX), Math.Min(sY, cY), w, h, 4);
                }
            }
            if (index == 7)
            {
                if (e.X >= listp[0].X - 10 && e.X <= listp[0].X + 10 && e.Y >= listp[0].Y - 10 && e.Y <= listp[0].Y + 10)
                {
                    if (listp.Count > 1)
                    {
                        if (index_style == 0)
                            g.DrawPolygon(pen, listp.ToArray());
                        if (index_style == 1)
                            g.FillPolygon(pen.Brush, listp.ToArray());
                        if (index_style == 2)
                        {
                            Pen p2 = new Pen(c2);
                            if (c2 == pen.Color)
                                p2.Color = c1;
                            g.FillPolygon(p2.Brush, listp.ToArray());
                            g.DrawPolygon(pen, listp.ToArray());
                        }
                    }
                    listp = null;
                }
                else
                {
                    if (listp[listp.Count - 1] != e.Location)
                    {
                        listp.Add(e.Location);
                        g.DrawLines(pen, listp.ToArray());
                    }
                }
            }
            if (index == 8)
            {
                listp.Add(e.Location);
                if (listp.Count == 4)
                {
                    g.DrawBezier(pen, listp[0], listp[2], listp[3], listp[1]);
                    listp = null;
                }
            }
            if (index == 9)
            {
                RichTextBox tb = new RichTextBox();
                tb.Location = new Point(Math.Min(sX, e.X), Math.Min(sY, e.Y));
                tb.Size = new Size(w, h);
                tb.Visible = true;
                tb.BackColor = Color.White;
                tb.ForeColor = pen.Color;
                tb.Leave += new EventHandler(tb_Leave);
                if (!isdone)
                {
                    pb_paint.Controls.Add(tb);
                    tb.Focus();
                    isdone = true;
                }
                else
                    btn_text.Focus();
            }
            if (index == 12)
            {
                Diamond dia = new Diamond();
                if (index_style == 0)
                    dia.DrawDiamond(g, pen, Math.Min(sX, cX), Math.Min(sY, cY), w, h);
                if (index_style == 1)
                    dia.FillDiamond(g, pen, Math.Min(sX, cX), Math.Min(sY, cY), w, h);
                if (index_style == 2)
                {
                    Pen p2 = new Pen(c2);
                    if (c2 == pen.Color)
                        p2.Color = c1;
                    dia.FillDiamond(g, p2, Math.Min(sX, cX), Math.Min(sY, cY), w, h);
                    dia.DrawDiamond(g, pen, Math.Min(sX, cX), Math.Min(sY, cY), w, h);
                }
            }
            lp = Point.Empty;
        }

        #endregion Mouse Up

        #region Mouse Move

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mdown && lp != Point.Empty)
            {
                if (index == 1)
                {
                    g.DrawLine(pen, lp, e.Location);
                    lp = e.Location;
                }
                if (index == 2)
                {
                    g.DrawLine(eraser, lp, e.Location);
                    lp = e.Location;
                }
            }
            pb_paint.Refresh();
            cX = e.X; cY = e.Y;
            w = Math.Max(e.X, sX) - Math.Min(e.X, sX);
            h = Math.Max(e.Y, sY) - Math.Min(e.Y, sY);
            lbl_point.Text = e.X + ", " + e.Y;
        }

        #endregion Mouse Move

        #region Mouse Click

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (index == 10)
            {
                if (e.Button == MouseButtons.Left)
                {
                    c1 = bm.GetPixel(e.X, e.Y);
                    pn_c1.BackColor = c1;
                }
                if (e.Button == MouseButtons.Right)
                {
                    c2 = bm.GetPixel(e.X, e.Y);
                    pn_c2.BackColor = c2;
                }
            }
            if (index == 11)
            {
                FillColor fc = new FillColor();
                if (e.Button == MouseButtons.Left)
                    fc.Fill(pb_paint, bm, e.X, e.Y, c1);
                if (e.Button == MouseButtons.Right)
                    fc.Fill(pb_paint, bm, e.X, e.Y, c2);
            }
        }

        #endregion Mouse Click

        #region Paint Event

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            if (mdown)
            {
                if (index == 3)
                {
                    if (index_style == 0)
                        g.DrawRectangle(pen, Math.Min(sX, cX), Math.Min(sY, cY), w, h);

                    if (index_style == 1)
                        g.FillRectangle(pen.Brush, Math.Min(sX, cX), Math.Min(sY, cY), w, h);
                    if (index_style == 2)
                    {
                        Pen p2 = new Pen(c2);
                        if (c2 == pen.Color)
                            p2.Color = c1;
                        g.FillRectangle(p2.Brush, Math.Min(sX, cX), Math.Min(sY, cY), w, h);
                        g.DrawRectangle(pen, Math.Min(sX, cX), Math.Min(sY, cY), w, h);
                    }
                }
                if (index == 4)
                {
                    if (index_style == 0)
                        g.DrawEllipse(pen, Math.Min(sX, cX), Math.Min(sY, cY), w, h);
                    if (index_style == 1)
                        g.FillEllipse(pen.Brush, Math.Min(sX, cX), Math.Min(sY, cY), w, h);
                    if (index_style == 2)
                    {
                        Pen p2 = new Pen(c2);
                        if (c2 == pen.Color)
                            p2.Color = c1;
                        g.FillEllipse(p2.Brush, Math.Min(sX, cX), Math.Min(sY, cY), w, h);
                        g.DrawEllipse(pen, Math.Min(sX, cX), Math.Min(sY, cY), w, h);
                    }
                }
                if (index == 5)
                {
                    g.DrawLine(pen, sX, sY, cX, cY);
                }
                if (index == 6)
                {
                    RoundedRetangle rr = new RoundedRetangle();
                    if (index_style == 0)
                        rr.DrawRoundedRectangle(g, pen, Math.Min(sX, cX), Math.Min(sY, cY), w, h, 4);
                    if (index_style == 1)
                        rr.FillRoundedRectangle(g, pen, Math.Min(sX, cX), Math.Min(sY, cY), w, h, 4);
                    if (index_style == 2)
                    {
                        Pen p2 = new Pen(c2);
                        if (c2 == pen.Color)
                            p2.Color = c1;
                        rr.FillRoundedRectangle(g, p2, Math.Min(sX, cX), Math.Min(sY, cY), w, h, 4);
                        rr.DrawRoundedRectangle(g, pen, Math.Min(sX, cX), Math.Min(sY, cY), w, h, 4);
                    }
                }
                if (index == 7)
                {
                    if (cX >= listp[0].X - 10 && cX <= listp[0].X + 10 && cY >= listp[0].Y - 10 && cY <= listp[0].Y + 10)
                    {
                        if (listp.Count > 1)
                        {
                            if (index_style == 0)
                                g.DrawPolygon(pen, listp.ToArray());
                            if (index_style == 1)
                                g.FillPolygon(pen.Brush, listp.ToArray());
                            if (index_style == 2)
                            {
                                Pen p2 = new Pen(c2);
                                if (c2 == pen.Color)
                                    p2.Color = c1;
                                g.FillPolygon(p2.Brush, listp.ToArray());
                                g.DrawPolygon(pen, listp.ToArray());
                            }
                        }
                    }
                    else
                        g.DrawLine(pen, listp[listp.Count - 1], new Point(cX, cY));
                }
                if (index == 8)
                {
                    if (listp.Count == 1)
                        g.DrawLine(pen, listp[0], new Point(cX, cY));
                    if (listp.Count == 2)
                        g.DrawBezier(pen, listp[0], new Point(cX, cY), new Point(cX, cY), listp[1]);
                    if (listp.Count == 3)
                        g.DrawBezier(pen, listp[0], listp[2], new Point(cX, cY), listp[1]);
                }
                if (index == 9)
                {
                    if (!isdone)
                        using (Pen p = new Pen(Color.Black, 3))
                        {
                            p.DashStyle = DashStyle.DashDot;
                            g.DrawRectangle(p, Math.Min(sX, cX), Math.Min(sY, cY), w, h);
                        }
                }
                if (index == 12)
                {
                    Diamond dia = new Diamond();
                    if (index_style == 0)
                        dia.DrawDiamond(g, pen, Math.Min(sX, cX), Math.Min(sY, cY), w, h);
                    if (index_style == 1)
                        dia.FillDiamond(g, pen, Math.Min(sX, cX), Math.Min(sY, cY), w, h);
                    if (index_style == 2)
                    {
                        Pen p2 = new Pen(c2);
                        if (c2 == pen.Color)
                            p2.Color = c1;
                        dia.FillDiamond(g, p2, Math.Min(sX, cX), Math.Min(sY, cY), w, h);
                        dia.DrawDiamond(g, pen, Math.Min(sX, cX), Math.Min(sY, cY), w, h);
                    }
                }
            }
            else
            {
                if (index == 8)
                {
                    if (listp != null)
                    {
                        if (listp.Count == 2)
                            g.DrawLine(pen, listp[0], listp[1]);
                        if (listp.Count == 3)
                            g.DrawBezier(pen, listp[0], listp[2], listp[2], listp[1]);
                    }
                }
            }
        }

        #endregion Paint Event

        #endregion Paint

        #region Button click

        private void color_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox pt = sender as PictureBox;
            if (e.Button == MouseButtons.Left)
            {
                c1 = pt.BackColor;
                pn_c1.BackColor = c1;
            }
            if (e.Button == MouseButtons.Right)
            {
                c2 = pt.BackColor;
                pn_c2.BackColor = c2;
            }
        }

        private void color_show_MouseClick(object sender, MouseEventArgs e)
        {
            Color temp = c1;
            c1 = c2;
            c2 = temp;
            pn_c1.BackColor = c1;
            pn_c2.BackColor = c2;
        }

        private void edit_Color_MouseClick(object sender, MouseEventArgs e)
        {
            if (cd.ShowDialog() == DialogResult.OK)
            {
                if (e.Button == MouseButtons.Left)
                {
                    c1 = cd.Color;
                    pn_c1.BackColor = c1;
                }
                if (e.Button == MouseButtons.Right)
                {
                    c2 = cd.Color;
                    pn_c2.BackColor = c2;
                }
            }
        }

        private void btn_pen_Click(object sender, EventArgs e)
        {
            index = 1;
        }

        private void btn_era_Click(object sender, EventArgs e)
        {
            index = 2;
        }

        private void btn_retan_Click(object sender, EventArgs e)
        {
            index = 3;
        }

        private void btn_Ellip_Click(object sender, EventArgs e)
        {
            index = 4;
        }

        private void btn_line_Click(object sender, EventArgs e)
        {
            index = 5;
        }

        private void btn_roretan_Click(object sender, EventArgs e)
        {
            index = 6;
        }

        private void btn_poly_Click(object sender, EventArgs e)
        {
            index = 7;
        }

        private void btn_curve_Click(object sender, EventArgs e)
        {
            index = 8;
        }

        private void brn_text_Click(object sender, EventArgs e)
        {
            index = 9;
        }

        private void btn_color_Click(object sender, EventArgs e)
        {
            index = 10;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            index = 11;
        }

        private void btn_diamond_Click(object sender, EventArgs e)
        {
            index = 12;
        }

        #endregion Button click

        #region menu click

        private void toolBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toolbox.Checked == true)
            {
                pn_tool.Visible = false;
                shape_style.Visible = false;
                line_style.Visible = false;
                lw.Visible = false;
                toolbox.Checked = false;
            }
            else
            {
                pn_tool.Visible = true;
                shape_style.Visible = true;
                line_style.Visible = true;
                lw.Visible = true;
                toolbox.Checked = true;
            }
        }

        private void editColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cd.ShowDialog() == DialogResult.OK)
            {
                c1 = cd.Color;
                pn_c1.BackColor = c1;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (drawed)
            {
                DialogResult result = MessageBox.Show("The change is unsaved. Do you want to save?", "Unsaved", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                    saveToolStripMenuItem_Click(sender, e);
                if (result == DialogResult.Cancel)
                    return;
            }
            Paint newform = new Paint();
            this.Hide();
            drawed = false;
            newform.ShowDialog();
            this.Close();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sv = new SaveFileDialog();
            sv.Title = "Save File";
            sv.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            sv.Filter = "JPeg Image|*.jpg|Png Image|*.png|Gif Image|*.gif";
            if (sv.ShowDialog() == DialogResult.OK)
            {
                if (sv.FileName != "")
                {
                    ImageFormat fm = ImageFormat.Jpeg;
                    switch (sv.FilterIndex)
                    {
                        case 2:
                            fm = ImageFormat.Png;
                            break;

                        case 3:
                            fm = ImageFormat.Gif;
                            break;
                    }

                    bm.Save(sv.FileName, fm);
                }
                drawed = false;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (drawed)
            {
                DialogResult result = MessageBox.Show("The change is unsaved. Do you want to save?", "Unsaved", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                    saveToolStripMenuItem_Click(sender, e);
                if (result == DialogResult.Cancel)
                    return;
            }
            this.Close();
        }

        private void colorbox_Click(object sender, EventArgs e)
        {
            if (colorbox.Checked == true)
            {
                color_show.Visible = false;
                pn_color.Visible = false;
                edit_Color.Visible = false;
                colorbox.Checked = false;
            }
            else
            {
                color_show.Visible = true;
                pn_color.Visible = true;
                edit_Color.Visible = true;
                colorbox.Checked = true;
            }
        }

        private void openToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog op = new OpenFileDialog())
            {
                op.Title = "Open Image";
                op.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                op.Filter = "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif|All files|*.*";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    pb_paint.Size = Image.FromFile(op.FileName).Size;
                    g = pb_paint.CreateGraphics();
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    bm = new Bitmap(op.FileName);
                    g = Graphics.FromImage(bm);
                    pb_paint.Image = bm;
                }
            }
        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (statusBarToolStripMenuItem.Checked == true)
            {
                lbl_point.Visible = false;
                lbl_sizedraw.Visible = false;
                label1.Visible = false;
                statusBarToolStripMenuItem.Checked = false;
                panel1.Height += 15;
            }
            else
            {
                lbl_point.Visible = true;
                lbl_sizedraw.Visible = true;
                label1.Visible = true;
                statusBarToolStripMenuItem.Checked = true;
                panel1.Height -= 15;
            }
        }

        private void resizeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (Resize rs = new Resize(this))
                rs.ShowDialog();
        }

        #endregion menu click

        #region Color and Design

        private void edit_Color_MouseLeave(object sender, EventArgs e)
        {
            edit_Color.BorderStyle = BorderStyle.FixedSingle;
        }

        private void color_show_MouseEnter(object sender, EventArgs e)
        {
            color_show.BorderStyle = BorderStyle.Fixed3D;
        }

        private void color_show_MouseLeave(object sender, EventArgs e)
        {
            color_show.BorderStyle = BorderStyle.FixedSingle;
        }

        private void edit_Color_MouseEnter(object sender, EventArgs e)
        {
            edit_Color.BorderStyle = BorderStyle.Fixed3D;
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            lbl_sizedraw.Text = pb_paint.Width + ", " + pb_paint.Height;
            bm = new Bitmap(pb_paint.Image, pb_paint.Width, pb_paint.Height);
            g = pb_paint.CreateGraphics();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g = Graphics.FromImage(bm);
            pb_paint.Image = bm;
        }

        private void btn_curve_Leave_1(object sender, EventArgs e)
        {
            if (listp != null)
            {
                if (listp.Count == 2)
                    g.DrawLine(pen, listp[0], listp[1]);
                if (listp.Count == 3)
                    g.DrawBezier(pen, listp[0], listp[2], listp[2], listp[1]);
            }
            listp = null;
        }

        private void btn_poly_Leave_1(object sender, EventArgs e)
        {
            listp = null;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            lbl_point.Text = "";
        }

        #endregion Color and Design

        #region style

        private void lw_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = Convert.ToInt32(lw.Value);
            eraser.Width = Convert.ToInt32(lw.Value) + 3;
        }

        private void line_style_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (line_style.SelectedIndex == 0)
                pen.DashStyle = DashStyle.Solid;
            if (line_style.SelectedIndex == 1)
                pen.DashStyle = DashStyle.Dash;
            if (line_style.SelectedIndex == 2)
                pen.DashStyle = DashStyle.DashDot;
            if (line_style.SelectedIndex == 3)
                pen.DashStyle = DashStyle.DashDotDot;
            if (line_style.SelectedIndex == 4)
                pen.DashStyle = DashStyle.Dot;
        }

        private void shape_style_SelectedIndexChanged(object sender, EventArgs e)
        {
            index_style = shape_style.SelectedIndex;
        }

        #endregion style

        #region etc

        private void Paint_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (drawed)
            {
                DialogResult result = MessageBox.Show("The change is unsaved. Do you want to save?", "Unsaved", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                    saveToolStripMenuItem_Click(sender, e);
                if (result == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void tb_Leave(object sender, EventArgs e)
        {
            RichTextBox tb = (RichTextBox)sender;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                g.DrawString(tb.Text, fd.Font, new SolidBrush(tb.ForeColor), tb.Location);
            }
            pb_paint.Controls.Remove(tb);
            isdone = false;
        }

        #endregion etc
    }
}