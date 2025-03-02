using Microsoft.AspNetCore.Mvc;
using P2FixAnAppDotNetCode9.Models;

namespace P2FixAnAppDotNetCode9.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private readonly Cart? _cart;

        public CartSummaryViewComponent(ICart cart)
        {
            _cart = cart as Cart;
        }

        public IViewComponentResult Invoke()
        {
            return View(_cart);
        }
    }
}
