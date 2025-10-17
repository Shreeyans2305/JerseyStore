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

        private void UpdateGreeting()
        {
            try
            {
                var name = Session.LoggedInUsername;
                if (string.IsNullOrEmpty(name))
                    label6.Text = "Hello, ";
                else
                    label6.Text = $"Hello, {name}";
            }
            catch
            {
                // ignore
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Set initial greeting from session
            UpdateGreeting();

            // Subscribe to changes so greeting updates if user signs out/in elsewhere
            if (!DesignMode)
            {
                try
                {
                    Session.UsernameChanged += Session_UsernameChanged;
                }
                catch
                {
                }
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            try
            {
                Session.UsernameChanged -= Session_UsernameChanged;
            }
            catch
            {
            }

            base.OnHandleDestroyed(e);
        }

        private void Session_UsernameChanged(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)UpdateGreeting);
            }
            else
            {
                UpdateGreeting();
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // simple direct assignment to the Designer-exposed label for product cards
            if (productCard1 != null)
                productCard1.Pricelbl.Text = "$19.00";
            if (productCard2 != null)
                productCard2.Pricelbl.Text = "$18.00";
            if (productCard3 != null)
                productCard3.Pricelbl.Text = "$29.00";
            if (productCard4 != null)
                productCard4.Pricelbl.Text = "$29.00";

            // Assign random prices in range 9..199 ending with .00
            try
            {
                AssignRandomPrice(productCard1);
                AssignRandomPrice(productCard2);
                AssignRandomPrice(productCard3);
                AssignRandomPrice(productCard4);
            }
            catch
            {
                // ignore
            }
        }

        private void AssignRandomPrice(ProductCard card)
        {
            if (card == null) return;
            // integer dollars between 9 and 199 inclusive
            int dollars = AppRandom.Rng.Next(9, 200);
            decimal price = dollars; // ends with .00
            card.Price = price;
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
