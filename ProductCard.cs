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
    public partial class ProductCard : UserControl
    {
        private int quantity = 1;

        public ProductCard()
        {
            InitializeComponent();
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

        // 🛒 Event: Raised when Add to Cart is clicked
        [Category("Action")]
        [Description("Fires when the Add to Cart button is clicked.")]
        public event EventHandler AddToCartClicked;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Quantity++;
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (Quantity > 1) Quantity--;
        }

        private void UpdateButtonText()
        {
            btnAddToCart.Text = $"Add {quantity} to cart";
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            AddToCartClicked?.Invoke(this, EventArgs.Empty);
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
        }
    }
}
