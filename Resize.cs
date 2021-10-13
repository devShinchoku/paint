using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTLON_paint
{
    public partial class Resize : Form
    {
        private Paint p;
        private int x, y;
        public Resize(Paint paint)
        {
            InitializeComponent();
            p = paint;
        }

        private void Resize_Load(object sender, EventArgs e)
        {
            rb_per.Checked = true;
            txt_X.Text = "100";
            txt_Y.Text = "100";
        }

        private void rb_per_CheckedChanged(object sender, EventArgs e)
        {
            txt_X.Text = "100";
            txt_Y.Text = "100";
        }

        private void rb_px_CheckedChanged(object sender, EventArgs e)
        {
            txt_X.Text = p.pb_paint.Width.ToString();
            txt_Y.Text = p.pb_paint.Height.ToString();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_X_TextChanged(object sender, EventArgs e)
        {
            try
            {
                x = int.Parse(txt_X.Text);
            }
            catch(FormatException fe)
            {
                MessageBox.Show("You can only type a number her", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_X.Text = p.pb_paint.Width.ToString();
            }
        }

        private void txt_Y_TextChanged(object sender, EventArgs e)
        {

            try
            {
                y = int.Parse(txt_X.Text);
            }
            catch (FormatException fe)
            {
                MessageBox.Show("You can only type a number her","Warning",MessageBoxButtons.OK ,MessageBoxIcon.Warning) ;
                txt_Y.Text = p.pb_paint.Height.ToString();
            }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (x < 1 || y < 1)
            {
                MessageBox.Show("Plese type a number larger than 0", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (rb_per.Checked == true)
                {
                    p.pb_paint.Width = (p.pb_paint.Width * x) / 100;
                    p.pb_paint.Height = (p.pb_paint.Height * y) / 100;
                }
                if (rb_px.Checked == true)
                    p.pb_paint.Size = new Size(x, y);
                this.Close();
            }
        }
    }
}
