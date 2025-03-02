﻿using P2FixAnAppDotNetCode9.Models.Repositories;

namespace P2FixAnAppDotNetCode9.Models.Services
{
    /// <summary>
    /// This class provides services to manages the products
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public ProductService(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Get all product from the inventory
        /// </summary>
        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts().ToList();
        }

        /// <summary>
        /// Get a product form the inventory by its id
        /// </summary>
        public Product GetProductById(int id)
        {
            // TODO implement the method
            return null;
        }

        /// <summary>
        /// Update the quantities left for each product in the inventory depending of ordered the quantities
        /// </summary>
        public void UpdateProductQuantities(Cart cart)
        {
            var lines = cart.Lines.ToArray();
            if (lines.Length == 0)
                return;

            foreach (var group in lines.GroupBy(g => g.Product))
            {
                var productId = group.Key.Id;
                _productRepository.UpdateProductStocks(productId, group.Select(s => s.Quantity).Sum());
            }
        }
    }
}
