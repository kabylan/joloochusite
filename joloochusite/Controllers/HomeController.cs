using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using joloochusite.Models;
using joloochusite.Data;
using Microsoft.AspNetCore.Identity;

namespace joloochusite.Controllers
{
    public class HomeController : Controller
    {
        SignInManager<ApplicationUser> SignInManager;
        UserManager<ApplicationUser> UserManager;
        ApplicationDbContext _context;

        public HomeController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            SignInManager = signInManager;
            UserManager = userManager;
        }

        public IActionResult Index()
        {

            if (SignInManager.IsSignedIn(User))
            {
                return RedirectToAction("Chat");
            }

            var userid = UserManager.GetUserId(User);

            if (userid != null)
            {
                ViewBag.UserId = userid;
            }
            else
            {
                ViewBag.UserId = "no user id";
            }

            ViewBag.PhoneNumber = UserManager.GetUserName(User);


            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
