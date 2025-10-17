using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace JerseyStore
{
    public class CartForm : Form
    {
        private DataGridView dgvCart;
        private Button btnBack;
        private Label lblTotal;
        private Button btnRefresh;

        public CartForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Your Cart";
            this.Size = new Size(800, 600);

            dgvCart = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            btnBack = new Button
            {
                Text = "Back",
                AutoSize = true,
                Anchor = AnchorStyles.Left
            };
            btnBack.Click += BtnBack_Click;

            btnRefresh = new Button
            {
                Text = "Refresh",
                AutoSize = true,
                Anchor = AnchorStyles.Left
            };
            btnRefresh.Click += BtnRefresh_Click;

            lblTotal = new Label
            {
                Text = "Total: $0.00",
                AutoSize = true,
                Font = new Font(Font.FontFamily, 12, FontStyle.Bold),
                Anchor = AnchorStyles.Right
            };

            FlowLayoutPanel topPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 40,
                Padding = new Padding(8),
                FlowDirection = FlowDirection.LeftToRight
            };
            topPanel.Controls.Add(btnBack);
            topPanel.Controls.Add(btnRefresh);
            topPanel.Controls.Add(new Label { Width = 20 });
            topPanel.Controls.Add(lblTotal);

            this.Controls.Add(dgvCart);
            this.Controls.Add(topPanel);

            this.Load += CartForm_Load;
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadCart();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            // navigate back to main form
            Form4 f = new Form4();
            this.Hide();
            f.Show();
        }

        private void CartForm_Load(object sender, EventArgs e)
        {
            LoadCart();
        }

        private void LoadCart()
        {
            try
            {
                if (!Session.IsSignedIn)
                {
                    MessageBox.Show("Please sign in to view your cart.");
                    // go to login
                    Form1 f = new Form1();
                    this.Hide();
                    f.Show();
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

                    // If price is stored as decimal, fine. Ensure correct types for subtotal calculation
                    dgvCart.DataSource = dt;

                    // Compute total as sum(qty * price)
                    decimal total = 0m;
                    foreach (DataRow row in dt.Rows)
                    {
                        try
                        {
                            int q = Convert.ToInt32(row["qty"]);
                            decimal p = Convert.ToDecimal(row["price"]);
                            total += q * p;
                        }
                        catch
                        {
                            // ignore malformed rows
                        }
                    }

                    lblTotal.Text = $"Total: ${total:0.00}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading cart: " + ex.Message);
            }
        }
    }
}
