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
    public partial class Form3 : Form
    {
        public Form3(Size size, Point location)
        {
            InitializeComponent();
            this.Size = size;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = location;
        }

        public Form3() : this(new Size(800, 600), new Point(100, 100)) // fallback/default
        {
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox3.Text != textBox4.Text)
            {
                MessageBox.Show("Please ensure that all the fields have been filled properly!");
                return;
            }
            else
            {
                MessageBox.Show("Account created successfully!");
                Form4 f = new Form4();
                this.Hide();
                f.Show();
            }
        }

        private void circleButton1_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2(this.Size, this.Location);
            this.Hide();
            f.Show();
        }
    }
}
