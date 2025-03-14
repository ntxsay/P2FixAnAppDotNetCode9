﻿namespace P2FixAnAppDotNetCode9.Models.Repositories
{
    /// <summary>
    /// The class that manages product data
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private static readonly List<Product> _products = [];
        
        static ProductRepository()
        {
            GenerateProductData();
        }

        /// <summary>
        /// Generate the default list of products
        /// </summary>
        private static void  GenerateProductData()
        {
            int id = 0;
            _products.Add(new Product(++id, 10, 92.50, "Echo Dot", "(2nd Generation) - Black"));
            _products.Add(new Product(++id, 20, 9.99, "Anker 3ft / 0.9m Nylon Braided", "Tangle-Free Micro USB Cable"));
            _products.Add(new Product(++id, 30, 69.99, "JVC HAFX8R Headphone", "Riptidz, In-Ear"));
            _products.Add(new Product(++id, 40, 32.50, "VTech CS6114 DECT 6.0", "Cordless Phone"));
            _products.Add(new Product(++id, 50, 895.00, "NOKIA OEM BL-5J", "Cell Phone "));
        }

        /// <summary>
        /// Retourne le produit spécifié par son Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product? GetProductById(int id)
        {
            return _products.SingleOrDefault(s => s.Id == id);
        }

        /// <summary>
        /// Get all products from the inventory
        /// </summary>
        public Product[] GetAllProducts()
        {
            var products = _products.Where(p => p.Stock > 0).OrderBy(p => p.Name).ToArray();
            return products;
        }

        /// <summary>
        /// Update the stock of a product in the inventory by its id
        /// </summary>
        public void UpdateProductStocks(int productId, int quantityToRemove)
        {
            Product product = _products.First(p => p.Id == productId);
            product.Stock -= quantityToRemove;

            if (product.Stock == 0)
                _products.Remove(product);
        }
    }
}
