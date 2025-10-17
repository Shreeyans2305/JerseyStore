using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace JerseyStore
{
    public partial class Form15 : Form
    {
        public Form15()
        {
            InitializeComponent();
            this.Load += Form15_Load;
            this.roundedButton1.Click += RoundedButton1_Click;
        }

        // Designer wired event handler — preserved for compatibility
        private void roundedButton1_Click(object sender, EventArgs e)
        {
            // Forward to the consolidated handler
            RoundedButton1_Click(sender, e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                try { Session.UsernameChanged += Session_UsernameChanged; } catch { }
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            try { Session.UsernameChanged -= Session_UsernameChanged; } catch { }
            base.OnHandleDestroyed(e);
        }

        private void Session_UsernameChanged(object sender, EventArgs e)
        {
            if (InvokeRequired) BeginInvoke((Action)ComputeTotal);
            else ComputeTotal();
        }

        private void Form15_Load(object sender, EventArgs e)
        {
            ComputeTotal();
        }

        private void ComputeTotal()
        {
            try
            {
                if (!Session.IsSignedIn)
                {
                    labelTotalValue.Text = "$0.00";
                    return;
                }

                string username = Session.LoggedInUsername;
                string connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\shree\\source\\repos\\JerseyStore\\Database1.mdf;Integrated Security=True";

                decimal total = 0m;

                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT qty, price FROM dbo.Table4 WHERE username = @u";
                    cmd.Parameters.Add(new SqlParameter("@u", System.Data.SqlDbType.NVarChar, 200) { Value = username });

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                int qty = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                                decimal price = reader.IsDBNull(1) ? 0m : reader.GetDecimal(1);
                                total += qty * price;
                            }
                            catch
                            {
                                // ignore malformed entry
                            }
                        }
                    }
                }

                labelTotalValue.Text = string.Format("${0:0.00}", total);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while computing total: " + ex.Message);
            }
        }

        private void RoundedButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Session.IsSignedIn)
                {
                    MessageBox.Show("Please sign in to place an order.");
                    return;
                }

                string username = Session.LoggedInUsername;
                string connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\shree\\source\\repos\\JerseyStore\\Database1.mdf;Integrated Security=True";

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlTransaction tx = conn.BeginTransaction())
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Transaction = tx;
                        try
                        {
                            // Insert into Table5 from Table4 for this user, with status 'processing'
                            cmd.CommandText = "INSERT INTO dbo.Table5 (username, product, qty, price, status) SELECT username, product, qty, price, @status FROM dbo.Table4 WHERE username = @u";
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add(new SqlParameter("@status", System.Data.SqlDbType.NVarChar, 50) { Value = "processing" });
                            cmd.Parameters.Add(new SqlParameter("@u", System.Data.SqlDbType.NVarChar, 200) { Value = username });

                            cmd.ExecuteNonQuery();

                            // Delete from Table4 for this user (clear cart)
                            cmd.CommandText = "DELETE FROM dbo.Table4 WHERE username = @u";
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add(new SqlParameter("@u", System.Data.SqlDbType.NVarChar, 200) { Value = username });

                            cmd.ExecuteNonQuery();

                            tx.Commit();

                            MessageBox.Show("Order Placed Successfully! You will recieve delivery instructions via email shortly.");

                            // Update total display
                            ComputeTotal();
                        }
                        catch (Exception ex)
                        {
                            try { tx.Rollback(); } catch { }
                            MessageBox.Show("An error occurred while placing order: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message);
            }
        }
    }
}
