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
    public partial class Form12 : Form
    {
        public Form12()
        {
            InitializeComponent();
        }

        private void circleButton1_Click(object sender, EventArgs e)
        {
            Form11 f = new Form11();
            this.Hide();
            f.Show();
        }

        private void circleButton2_Click(object sender, EventArgs e)
        {
            Form13 f = new Form13();
            this.Hide();
            f.Show();
        }
    }
}
