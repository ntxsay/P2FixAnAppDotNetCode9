﻿namespace P2FixAnAppDotNetCode9.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {
        /// <summary>
        /// Liste actuelle des paniers
        /// </summary>
        private readonly List<CartLine> _cartLinesList = [];
        
        /// <summary>
        /// Read-only property for dispaly only
        /// </summary>
        public IEnumerable<CartLine> Lines => _cartLinesList;
        

        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>//
        public void AddItem(Product product, int quantity)
        {
            if (quantity <= 0)
                return;
            
            var cartLine = _cartLinesList.SingleOrDefault(f => f.Product.Id == product.Id);
            if (cartLine == null)
            {
                cartLine = new CartLine()
                {
                    Product = product,
                    Quantity = quantity
                };
                
                _cartLinesList.Add(cartLine);
            }
            else
            {
                cartLine.Quantity += quantity;
            }
        }

        /// <summary>
        /// Removes a product form the cart
        /// </summary>
        public void RemoveLine(Product product) =>
            _cartLinesList.RemoveAll(l => l.Product.Id == product.Id);

        /// <summary>
        /// Get total value of a cart
        /// </summary>
        public double GetTotalValue() => _cartLinesList.Count == 0 
            ? 0d
            : _cartLinesList.Select(s => s.Product.Price * s.Quantity).Sum();

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue()
        {
            return _cartLinesList.Count == 0
                ? 0d
                : _cartLinesList.Select(s => s.Product.Price * s.Quantity).Sum() /
                  _cartLinesList.Select(s => s.Quantity).Sum();
        }

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public Product? FindProductInCartLines(int productId)
        {
            return _cartLinesList.SingleOrDefault(l => l.Product.Id == productId)?.Product;
        }

        /// <summary>
        /// Get a specifid cartline by its index
        /// </summary>
        public CartLine? GetCartLineByIndex(int index)
        {
            return index < 0 || index > _cartLinesList.Count - 1
                ? null
                : _cartLinesList[index];
        }

        /// <summary>
        /// Clears a the cart of all added products
        /// </summary>
        public void Clear()
        {
            _cartLinesList.Clear();
        }
    }

    public class CartLine
    {
        public int OrderLineId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
