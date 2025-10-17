using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace JerseyStore
{
    public partial class Form16 : Form
    {
        public Form16()
        {
            InitializeComponent();
            this.Load += Form16_Load;
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
            if (InvokeRequired) BeginInvoke((Action)LoadOrders);
            else LoadOrders();
        }

        private void Form16_Load(object sender, EventArgs e)
        {
            LoadOrders();
        }

        private void LoadOrders()
        {
            try
            {
                if (!Session.IsSignedIn)
                {
                    dataGridView1.DataSource = null;
                    return;
                }

                string username = Session.LoggedInUsername;
                string connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\shree\\source\\repos\\JerseyStore\\Database1.mdf;Integrated Security=True";

                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = conn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    // select all columns except username by selecting specific columns if known; fallback to select * and remove column
                    cmd.CommandText = "SELECT * FROM dbo.Table5 WHERE username = @u";
                    cmd.Parameters.Add(new SqlParameter("@u", System.Data.SqlDbType.NVarChar, 200) { Value = username });

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Remove username column if present
                    if (dt.Columns.Contains("username"))
                        dt.Columns.Remove("username");

                    // Bind to grid
                    dataGridView1.DataSource = dt;

                    // Format price column if present
                    if (dataGridView1.Columns.Contains("price"))
                    {
                        dataGridView1.Columns["price"].DefaultCellStyle.Format = "C2";
                        dataGridView1.Columns["price"].DefaultCellStyle.FormatProvider = System.Globalization.CultureInfo.GetCultureInfo("en-US");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading orders: " + ex.Message);
            }
        }
    }
}
