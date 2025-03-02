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

            Assert.Equal(9, products.First(p => p.Id == 1).Stock);
            Assert.Equal(28, products.First(p => p.Id == 3).Stock);
            Assert.Equal(47, products.First(p => p.Id == 5).Stock);

            //do a second run adding items to cart. Resetting the repo and service and cart
            //will simulate the process from the front end perspective
            //here testing that product stock values are decreasing for each cart checkout, not just a single time
            cart = new Cart();
            productRepository = new ProductRepository();
            productService = new ProductService(productRepository, orderRepository);
            products = productService.GetAllProducts();
            cart.AddItem(products.Where(p => p.Id == 1).First(), 1);
            cart.AddItem(products.Where(p => p.Id == 3).First(), 2);
            cart.AddItem(products.Where(p => p.Id == 5).First(), 3);
            productService.UpdateProductQuantities(cart);
            Assert.Equal(8, products.Where(p => p.Id == 1).First().Stock);
            Assert.Equal(26, products.Where(p => p.Id == 3).First().Stock);
            Assert.Equal(44, products.Where(p => p.Id == 5).First().Stock);
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
