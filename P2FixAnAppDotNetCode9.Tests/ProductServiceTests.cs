using P2FixAnAppDotNetCode9.Models;
using P2FixAnAppDotNetCode9.Models.Repositories;
using P2FixAnAppDotNetCode9.Models.Services;

namespace P2FixAnAppDotNetCode9.Tests
{
    /// <summary>
    /// The ProductService test class
    /// </summary>
    public class ProductServiceTests
    {

        [Fact]
        public void Product()
        {
            IProductRepository productRepository = new ProductRepository();
            IOrderRepository orderRepository = new OrderRepository();
            IProductService productService = new ProductService(productRepository, orderRepository);

            var products = productService.GetAllProducts();

            Assert.IsType<List<Product>>(products);
        }

        [Fact]
        public void UpdateProductQuantities()
        {
            Cart cart = new Cart();
            IProductRepository productRepository = new ProductRepository();
            IOrderRepository orderRepository = new OrderRepository();
            IProductService productService = new ProductService(productRepository, orderRepository);

            IEnumerable<Product> products = productService.GetAllProducts();
            cart.AddItem(products.First(p => p.Id == 1), 1);
            cart.AddItem(products.First(p => p.Id == 3), 2);
            cart.AddItem(products.First(p => p.Id == 5), 3);

            productService.UpdateProductQuantities(cart);

            var productIdStock = new Dictionary<int, int>()
            {
                { 1, products.First(p => p.Id == 1).Stock },
                { 3, products.First(p => p.Id == 3).Stock },
                { 5, products.First(p => p.Id == 5).Stock }
            };
            
            Assert.Equal(9, productIdStock.GetValueOrDefault(1));
            Assert.Equal(28, productIdStock.GetValueOrDefault(3));
            Assert.Equal(47, productIdStock.GetValueOrDefault(5));

            //do a second run adding items to cart. Resetting the repo and service and cart
            //will simulate the process from the front end perspective
            //here testing that product stock values are decreasing for each cart checkout, not just a single time
            cart = new Cart();
            productRepository = new ProductRepository();
            productService = new ProductService(productRepository, orderRepository);
            products = productService.GetAllProducts();

            products.First(p => p.Id == 1).Stock = productIdStock.GetValueOrDefault(1);
            products.First(p => p.Id == 3).Stock = productIdStock.GetValueOrDefault(3);
            products.First(p => p.Id == 5).Stock = productIdStock.GetValueOrDefault(5);
            
            cart.AddItem(products.First(p => p.Id == 1), 1);
            cart.AddItem(products.First(p => p.Id == 3), 2);
            cart.AddItem(products.First(p => p.Id == 5), 3);
            productService.UpdateProductQuantities(cart);

            productIdStock[1] = products.First(p => p.Id == 1).Stock;
            productIdStock[3] = products.First(p => p.Id == 3).Stock;
            productIdStock[5] = products.First(p => p.Id == 5).Stock;
            
            Assert.Equal(8, productIdStock.GetValueOrDefault(1));
            Assert.Equal(26, productIdStock.GetValueOrDefault(3));
            Assert.Equal(44, productIdStock.GetValueOrDefault(5));
        }

        [Fact]
        public void GetProductById()
        {
            IProductRepository productRepository = new ProductRepository();
            IOrderRepository orderRepository = new OrderRepository();
            IProductService productService = new ProductService(productRepository, orderRepository);
            int id = 3;

            Product product = productService.GetProductById(id);

            Assert.Same("JVC HAFX8R Headphone", product.Name);
            Assert.Equal(69.99, product.Price);
        }
    }
}
