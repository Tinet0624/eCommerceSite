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
            // Add product to the cart cookie
            string data = JsonConvert.SerializeObject(p);
            CookieOptions options = new CookieOptions() // Chocolate?
            {
                Expires = DateTime.Now.AddYears(1),
                Secure = true,
                IsEssential = true
            };
            _httpContext.HttpContext.Response.Cookies.Append("CartCookie", data, options);
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
