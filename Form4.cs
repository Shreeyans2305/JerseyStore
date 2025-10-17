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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }


        private void Form4_Load(object sender, EventArgs e)
        {
            // simple direct assignment to the Designer-exposed label
            if (productCard1 != null)
                productCard1.Pricelbl.Text = "$19.99";
            if (productCard2 != null)
                productCard2.Pricelbl.Text = "$18.99";
            if (productCard3 != null)
                productCard3.Pricelbl.Text = "$29.99";
            if (productCard4 != null)
                productCard4.Pricelbl.Text = "$29.99";
        }

        private void ProductCard1_AddToCartClicked(object sender, EventArgs e)
        {
            MessageBox.Show("Added to cart");
        }

        private void productCard1_Load(object sender, EventArgs e)
        {

        }

        private void circleButton2_Click(object sender, EventArgs e)
        {
            Form5 f = new Form5();
            this.Hide();
            f.Show();
        }
    }
}
