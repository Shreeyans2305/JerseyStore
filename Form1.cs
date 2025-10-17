using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JerseyStore
{
    public partial class Form1 : Form
    {
        private const int BottomMargin = 40; // Distance from bottom

        public Form1()
        {
            InitializeComponent();
            this.Resize += Form1_Resize;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            CenterRoundedButton();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            CenterRoundedButton();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            CenterRoundedButton();
        }

        private void CenterRoundedButton()
        {
            if (roundedButton1 == null) return;

            // Calculate margin: 10% of height, but at least 40px
            int dynamicMargin = Math.Max(40, (int)(this.ClientSize.Height * 0.10));

            int x = (this.ClientSize.Width - roundedButton1.Width) / 2;
            int y = this.ClientSize.Height - roundedButton1.Height - dynamicMargin;
            roundedButton1.Location = new Point(x, y);
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2(this.Size, this.Location);
            this.Hide();
            f.Show();
        }
    }
}
