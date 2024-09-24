using Ecommerce.Helper;
using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();
            return View("~/Views/Cart/CartPanel.cshtml", new CartModel
            {   
                quantity = cart.Sum(p =>p.SoLuong),
                total = cart.Sum(p => p.ThanhTien)
            });
        }
    }
}
