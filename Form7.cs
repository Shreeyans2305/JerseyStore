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
    public partial class Form7 : Form
    {
        public Form7()
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
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateGreeting();
            if (!DesignMode)
            {
                try { Session.UsernameChanged += Session_UsernameChanged; } catch { }
            }

            try
            {
                AssignRandomPrice(productCard1);
                AssignRandomPrice(productCard2);
                AssignRandomPrice(productCard3);
            }
            catch { }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            try { Session.UsernameChanged -= Session_UsernameChanged; } catch { }
            base.OnHandleDestroyed(e);
        }

        private void Session_UsernameChanged(object sender, EventArgs e)
        {
            if (InvokeRequired) BeginInvoke((Action)UpdateGreeting);
            else UpdateGreeting();
        }

        private void AssignRandomPrice(ProductCard card)
        {
            if (card == null) return;
            int dollars = AppRandom.Rng.Next(9, 200);
            card.Price = dollars;
        }

        private void circleButton1_Click(object sender, EventArgs e)
        {
            Form6 f = new Form6();
            this.Hide();
            f.Show();
        }

        private void circleButton2_Click(object sender, EventArgs e)
        {
            Form8 f = new Form8();
            this.Hide();
            f.Show();
        }
    }
}
