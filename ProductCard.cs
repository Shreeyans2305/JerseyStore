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
using System.Globalization;

namespace JerseyStore
{
    public partial class ProductCard : UserControl
    {
        private int quantity = 1;

        public ProductCard()
        {
            InitializeComponent();
        }

        // New Price property (decimal). Setting updates the label text with currency format and .00 precision.
        private decimal _price = 0m;
        [Category("Product Info")]
        [Description("Price of the product (decimal). Setting updates the price label text.")]
        public decimal Price
        {
            get => _price;
            set
            {
                _price = Math.Round(value, 2);
                UpdatePriceLabel();
            }
        }

        private void UpdatePriceLabel()
        {
            try
            {
                if (this.Pricelbl != null)
                {
                    // Ensure it ends with .00 when price is whole dollars
                    Pricelbl.Text = string.Format(CultureInfo.GetCultureInfo("en-US"), "${0:0.00}", _price);
                }
            }
            catch
            {
                // ignore
            }
        }

        // 🖼 Product Image Property
        [Category("Product Info")]
        [Description("Sets the product image displayed on the card.")]
        [Browsable(true)]
        public Image ProductImage
        {
            get => pictureBoxProduct.Image;
            set => pictureBoxProduct.Image = value;
        }

        // 📝 Product Title Property (renamed to avoid hiding Control.ProductName)
        [Category("Product Info")]
        [Description("Sets the product name displayed under the image.")]
        [Browsable(true)]
        public string ProductTitle
        {
            get => lblProductName.Text;
            set => lblProductName.Text = value;
        }

        // 🔢 Quantity Property
        [Category("Product Info")]
        [Description("Initial quantity for the add to cart button.")]
        public int Quantity
        {
            get => quantity;
            set
            {
                if (value < 1) value = 1;
                quantity = value;
                UpdateButtonText();
            }
        }

        // 🛒 Event: Raised when Add to Cart is clicked (legacy)
        [Category("Action")]
        [Description("Fires when the Add to Cart button is clicked.")]
        public event EventHandler AddToCartClicked;

        // 🛒 New event that includes current quantity
        [Category("Action")]
        [Description("Fires when the Add to Cart button is clicked and provides the selected quantity.")]
        public event EventHandler<QuantityEventArgs> AddToCartWithQuantity;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // increment quantity and update text
            Quantity++;
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (Quantity > 1) Quantity--;
        }

        private void UpdateButtonText()
        {
            // show the current quantity on the AddToCart button
            btnAddToCart.Text = $"Add {quantity} to cart";
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            // raise both events for compatibility
            AddToCartClicked?.Invoke(this, EventArgs.Empty);
            AddToCartWithQuantity?.Invoke(this, new QuantityEventArgs(quantity));

            // Also persist cart row into database
            try
            {
                // Require user to be signed in
                if (!Session.IsSignedIn)
                {
                    MessageBox.Show("Please sign in to add items to your cart.");
                    return;
                }

                string username = Session.LoggedInUsername;
                string product = lblProductName?.Text ?? ProductTitle ?? string.Empty;
                int qty = quantity;

                // Use Price property
                decimal priceValue = _price;

                // Connection string - keep in sync with other forms
                string connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\shree\\source\\repos\\JerseyStore\\Database1.mdf;Integrated Security=True";

                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO dbo.Table4 (username, product, qty, price) VALUES (@u, @p, @q, @pr)";
                    cmd.Parameters.Add(new SqlParameter("@u", SqlDbType.NVarChar, 200) { Value = username });
                    cmd.Parameters.Add(new SqlParameter("@p", SqlDbType.NVarChar, 500) { Value = product });
                    cmd.Parameters.Add(new SqlParameter("@q", SqlDbType.Int) { Value = qty });
                    var prParam = new SqlParameter("@pr", SqlDbType.Decimal) { Value = priceValue };
                    // Optionally set precision/scale if needed
                    prParam.Precision = 18;
                    prParam.Scale = 2;
                    cmd.Parameters.Add(prParam);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();

                    if (rows == 1)
                    {
                        MessageBox.Show("Item added to cart.");
                    }
                    else
                    {
                        MessageBox.Show("Unable to add item to cart. Please try again.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while adding to cart: " + ex.Message);
            }
        }

        // Designer-referenced event handlers
        private void panelMain_Paint(object sender, PaintEventArgs e)
        {
            // Keep empty or add custom painting if needed. Leaving empty prevents designer errors.
        }

        private void ProductCard_Load(object sender, EventArgs e)
        {
            // Ensure button text reflects initial quantity when control is loaded in designer/runtime.
            UpdateButtonText();
            UpdatePriceLabel();
        }
    }

    public class QuantityEventArgs : EventArgs
    {
        public int Quantity { get; }
        public QuantityEventArgs(int quantity) => Quantity = quantity;
    }
}
