using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace JerseyStore
{
    public partial class Form2 : Form
    {
        public Form2(Size size, Point location)
        {
            InitializeComponent();
            this.Size = size;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = location;
        }

        public Form2() : this(new Size(800, 600), new Point(100, 100)) // fallback/default
        {
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            string user = textBox1.Text.Trim();
            string pass = textBox2.Text; // consider hashing in real apps

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            // Inline connection string. Update Data Source / Initial Catalog / authentication as appropriate for your environment.
            string connStr = "Server=.;Initial Catalog=db0;Integrated Security=True;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT COUNT(1) FROM dbo.Table1 WHERE username = @u AND password = @p";
                    cmd.Parameters.Add(new SqlParameter("@u", SqlDbType.NVarChar, 100) { Value = user });
                    cmd.Parameters.Add(new SqlParameter("@p", SqlDbType.NVarChar, 100) { Value = pass });

                    conn.Open();
                    int matches = (int)cmd.ExecuteScalar();

                    if (matches == 1)
                    {
                        // login successful
                        Form4 f = new Form4();
                        this.Hide();
                        f.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while trying to log in: " + ex.Message);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form3 f = new Form3(this.Size, this.Location);
            this.Hide();
            f.Show();
        }
    }
}
