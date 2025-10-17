using System;
using System.Drawing;

namespace JerseyStore
{
    public class AddToCartEventArgs : EventArgs
    {
        public int Quantity { get; }
        public string Description { get; }
        public Image ProductImage { get; }

        public AddToCartEventArgs(int quantity, string description, Image productImage)
        {
            Quantity = quantity;
            Description = description;
            ProductImage = productImage;
        }
    }
}
