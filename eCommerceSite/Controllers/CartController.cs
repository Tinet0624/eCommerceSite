using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductContext _context;
        private readonly IHttpContextAccessor _httpContext;
        public CartController(ProductContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Add a product to the shopping cart
        /// </summary>
        /// <param name="id">The Id of the product to add</param>
        /// <returns></returns>
        public async Task<IActionResult> Add(int id)
        {
            Product p = await ProductDB.GetSingleProductAsync(_context, id);
            const string CartCookie = "CartCookie";

            // Get exsisting cart items
            string exsistingCart = _httpContext.HttpContext.Request.Cookies[CartCookie];
            List<Product> cartProducts = new List<Product>();
            if (exsistingCart != null)
            {
                cartProducts = JsonConvert.DeserializeObject<List<Product>>(exsistingCart);
            }
            // Add current product to exsisting cart
            cartProducts.Add(p);

            // Add products to the cart cookie
            string data = JsonConvert.SerializeObject(cartProducts);

            // Cookie Options
            CookieOptions options = new CookieOptions() // Chocolate?
            {
                Expires = DateTime.Now.AddYears(1),
                Secure = true,
                IsEssential = true
            };
            _httpContext.HttpContext.Response.Cookies.Append(CartCookie, data, options);
            // redirect to preivious page
            return RedirectToAction("Index", "Product");
        }

        public IActionResult Summary()
        {
            // Display all products in the shopping cart cookie
            return View();
        }
    }
}
