using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Add(int id) // This is the ID of the product to add
        {
            // Get the product from DB using ID
            // Add product to the cart cookie

            // redirect to preivious page
            return View();
        }

        public IActionResult Summary()
        {
            // Display all products in the shopping cart cookie
            return View();
        }
    }
}
