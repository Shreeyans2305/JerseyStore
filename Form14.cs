using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace JerseyStore
{
    public partial class Form14 : Form
    {
        public Form14()
        {
            InitializeComponent();
            this.Load += Form14_Load;
            this.dataGridView1.CellContentClick += DataGridView1_CellContentClick;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // subscribe to session changes to refresh cart when user signs in/out
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
            if (InvokeRequired) BeginInvoke((Action)LoadCart);
            else LoadCart();
        }

        private void Form14_Load(object sender, EventArgs e)
        {
            // ensure grid has a Remove button column
            EnsureRemoveButtonColumn();
            LoadCart();
        }

        private void EnsureRemoveButtonColumn()
        {
            const string removeColumnName = "RemoveColumn";
            if (!dataGridView1.Columns.Contains(removeColumnName))
            {
                var btnCol = new DataGridViewButtonColumn();
                btnCol.Name = removeColumnName;
                btnCol.HeaderText = "";
                btnCol.Text = "Remove";
                btnCol.UseColumnTextForButtonValue = true;
                btnCol.Width = 80;
                dataGridView1.Columns.Add(btnCol);
            }
        }

        private void LoadCart()
        {
            try
            {
                if (!Session.IsSignedIn)
                {
                    dataGridView1.DataSource = null;
                    MessageBox.Show("Please sign in to view your cart.");
                    return;
                }

                string username = Session.LoggedInUsername;
                string connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\shree\\source\\repos\\JerseyStore\\Database1.mdf;Integrated Security=True";

                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = conn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandText = "SELECT product, qty, price FROM dbo.Table4 WHERE username = @u";
                    cmd.Parameters.Add(new SqlParameter("@u", System.Data.SqlDbType.NVarChar, 200) { Value = username });

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;

                    // move Remove column to the end
                    EnsureRemoveButtonColumn();
                    var col = dataGridView1.Columns["RemoveColumn"];
                    if (col != null)
                    {
                        col.DisplayIndex = dataGridView1.Columns.Count - 1;
                    }

                    // Optional: format price column
                    if (dataGridView1.Columns.Contains("price"))
                    {
                        dataGridView1.Columns["price"].DefaultCellStyle.Format = "C2";
                        dataGridView1.Columns["price"].DefaultCellStyle.FormatProvider = System.Globalization.CultureInfo.GetCultureInfo("en-US");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading cart: " + ex.Message);
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var grid = dataGridView1;
            if (grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                // Remove button clicked for this row
                try
                {
                    var row = grid.Rows[e.RowIndex];
                    string product = Convert.ToString(row.Cells["product"].Value ?? string.Empty);
                    int qty = 0;
                    decimal price = 0m;
                    try { qty = Convert.ToInt32(row.Cells["qty"].Value); } catch { qty = 1; }
                    try { price = Convert.ToDecimal(row.Cells["price"].Value); } catch { price = 0m; }

                    var confirm = MessageBox.Show($"Remove '{product}' (qty {qty}) from your cart?", "Confirm remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm != DialogResult.Yes) return;

                    DeleteOneCartRow(Session.LoggedInUsername, product, qty, price);
                    LoadCart();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while removing item: " + ex.Message);
                }
            }
        }

        private void DeleteOneCartRow(string username, string product, int qty, decimal price)
        {
            try
            {
                string connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\shree\\source\\repos\\JerseyStore\\Database1.mdf;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // Delete one matching row. DELETE TOP (1) works in SQL Server to remove a single row.
                    cmd.CommandText = "DELETE TOP (1) FROM dbo.Table4 WHERE username = @u AND product = @p AND qty = @q AND price = @pr";
                    cmd.Parameters.Add(new SqlParameter("@u", System.Data.SqlDbType.NVarChar, 200) { Value = username });
                    cmd.Parameters.Add(new SqlParameter("@p", System.Data.SqlDbType.NVarChar, 500) { Value = product });
                    cmd.Parameters.Add(new SqlParameter("@q", System.Data.SqlDbType.Int) { Value = qty });
                    cmd.Parameters.Add(new SqlParameter("@pr", System.Data.SqlDbType.Decimal) { Value = price });

                    conn.Open();
                    int affected = cmd.ExecuteNonQuery();
                    if (affected == 0)
                    {
                        MessageBox.Show("No matching cart row found to remove.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting cart row: " + ex.Message);
            }
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            Form15 f = new Form15();
            this.Hide();
            f.Show();
        }
    }
}
