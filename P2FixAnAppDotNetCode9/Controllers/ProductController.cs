using Microsoft.AspNetCore.Mvc;
using P2FixAnAppDotNetCode9.Models;
using P2FixAnAppDotNetCode9.Models.Services;

namespace P2FixAnAppDotNetCode9.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILanguageService _languageService;

        public ProductController(IProductService productService, ILanguageService languageService)
        {
            _productService = productService;
            _languageService = languageService;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            return View(products);
        }
    }
}