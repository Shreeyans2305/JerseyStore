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
    public partial class MenuStrip : UserControl
    {
        public MenuStrip()
        {
            InitializeComponent();
        }

        private void accToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Hide();
            f.Show();

        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Hide();
            f.Show();
        }

        private void nikeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 f = new Form6();
            Parent.Hide();
            f.Show();
        }

        private void adidasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form7 f = new Form7();
            Parent.Hide();
            f.Show();
        }

        private void pumaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form8 f = new Form8();
            Parent.Hide();
            f.Show();
        }

        private void newBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form9 f = new Form9();
            Parent.Hide();
            f.Show();
        }

        private void f1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form10 f = new Form10();
            Parent.Hide();
            f.Show();
        }

        private void cricketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form11 f = new Form11();
            Parent.Hide();
            f.Show();
        }

        private void footballToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form12 f = new Form12();
            Parent.Hide();
            f.Show();
        }

        private void basketballToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form13 f = new Form13();
            Parent.Hide();
            f.Show();
        }
    }
}
