using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.Data;
using Store.Models;
using Store.Models.ViewModels;
using Store.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDBContext _db;

        public HomeController(ILogger<HomeController> logger,ApplicationDBContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                Products = _db.Products.Include(u => u.Category),
                Categories = _db.Categories
            };
            return View(homeVM);
        }
        public IActionResult Details(int?id)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();

            if(HttpContext.Session.Get<IEnumerable<ShoppingCart>>(ENV.SessinCart)!=null&&
                HttpContext.Session.Get<IEnumerable<ShoppingCart>>(ENV.SessinCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(ENV.SessinCart);
            }
            DeteilsVM deteilsVM = new DeteilsVM()
            {
                Product = _db.Products.Include(u => u.Category).Where(u => u.Id == id).FirstOrDefault(),
                inCart=false
            };
           foreach(var item in shoppingCartList)
            {
                if (item.ProductId == id)
                {
                    deteilsVM.inCart = true;
                }
            }
            return View(deteilsVM);
        }
        [HttpPost, ActionName("Details")]
        public IActionResult DetailsPost(int id)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(ENV.SessinCart) != null &&
                HttpContext.Session.Get<IEnumerable<ShoppingCart>>(ENV.SessinCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(ENV.SessinCart);
            }
            shoppingCartList.Add(new ShoppingCart { ProductId = id });
            HttpContext.Session.Set(ENV.SessinCart, shoppingCartList);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult RemoveFromCart(int id)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(ENV.SessinCart) != null &&
                HttpContext.Session.Get<IEnumerable<ShoppingCart>>(ENV.SessinCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(ENV.SessinCart);
            }

            var cart = shoppingCartList.FirstOrDefault(u => u.ProductId == id);
            if (cart != null)
            {
                shoppingCartList.Remove(cart);
            }
            HttpContext.Session.Set(ENV.SessinCart, shoppingCartList);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
