using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

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
                // Prepare values
                string user = textBox1.Text.Trim();
                string email = textBox2.Text.Trim();
                string pass = textBox3.Text; // consider hashing in real apps

                // Inline connection string - keep in sync with Form2
                string connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\shree\\source\\repos\\JerseyStore\\Database1.mdf;Integrated Security=True";

                try
                {
                    using (SqlConnection conn = new SqlConnection(connStr))
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        conn.Open();

                        // Check for existing username
                        cmd.CommandText = "SELECT COUNT(1) FROM dbo.Table3 WHERE username = @u";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@u", SqlDbType.NVarChar, 100) { Value = user });

                        int exists = (int)cmd.ExecuteScalar();
                        if (exists > 0)
                        {
                            MessageBox.Show("Username already exists. Please choose a different username.");
                            return;
                        }

                        // Insert new user
                        cmd.CommandText = "INSERT INTO dbo.Table3 (username, email, password) VALUES (@u, @e, @p)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@u", SqlDbType.NVarChar, 100) { Value = user });
                        cmd.Parameters.Add(new SqlParameter("@e", SqlDbType.NVarChar, 200) { Value = email });
                        cmd.Parameters.Add(new SqlParameter("@p", SqlDbType.NVarChar, 100) { Value = pass });

                        int rows = cmd.ExecuteNonQuery();

                        if (rows == 1)
                        {
                            MessageBox.Show("Account created successfully!");
                            Session.SignIn(user);
                            Form4 f = new Form4();
                            this.Hide();
                            f.Show();
                        }
                        else
                        {
                            MessageBox.Show("Unable to create account. Please try again.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while creating account: " + ex.Message);
                }
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
