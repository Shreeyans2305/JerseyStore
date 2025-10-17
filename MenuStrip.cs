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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            try
            {
                // Initialize account menu text
                UpdateAccountText();

                // Subscribe to session changes (only at runtime)
                if (!DesignMode)
                    Session.UsernameChanged += Session_UsernameChanged;
            }
            catch
            {
                // ignore
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
                // ignore
            }

            base.OnHandleDestroyed(e);
        }

        private void Session_UsernameChanged(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)UpdateAccountText);
            }
            else
            {
                UpdateAccountText();
            }
        }

        private void UpdateAccountText()
        {
            try
            {
                if (accountToolStripMenuItem == null)
                    return;

                if (Session.IsSignedIn)
                    accountToolStripMenuItem.Text = $"Account ({Session.LoggedInUsername})";
                else
                    accountToolStripMenuItem.Text = "Account";
            }
            catch
            {
                // ignore
            }
        }

        private void accToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // sign out from global session
                Session.SignOut();
            }
            catch
            {
                // ignore
            }

            // navigate to main form (login / start)
            Form1 f = new Form1();
            if (this.Parent != null)
                this.Parent.Hide();
            else
                this.Hide();
            f.Show();

        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            Parent.Hide();
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

        private void viewCartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form14 f = new Form14();
            Parent.Hide();
            f.Show();
        }

        private void checkoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form15 f = new Form15();
            Parent.Hide();
            f.Show();
        }

        private void viewOrderHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form16 f = new Form16();
            Parent.Hide();
            f.Show();
        }
    }
}
