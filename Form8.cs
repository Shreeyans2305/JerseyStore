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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void circleButton1_Click(object sender, EventArgs e)
        {
            Form7 f = new Form7();
            this.Hide();
            f.Show();
        }

        private void circleButton2_Click(object sender, EventArgs e)
        {
            Form9 f = new Form9();
            this.Hide();
            f.Show();
        }
    }
}
