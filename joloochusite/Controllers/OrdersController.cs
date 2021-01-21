using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using joloochusite.Data;
using joloochusite.Model;
using joloochusite.Models;
using Microsoft.AspNetCore.Identity;

namespace joloochusite.Controllers
{
    public class OrdersController : Controller
    {
        SignInManager<ApplicationUser> SignInManager;
        UserManager<ApplicationUser> UserManager;
        ApplicationDbContext _context;

        public OrdersController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            SignInManager = signInManager;
            UserManager = userManager;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Orders.Include(o => o.Car).Include(o => o.EndPoint).Include(o => o.StartPoint);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Car)
                .Include(o => o.EndPoint)
                .Include(o => o.StartPoint)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {

            ViewData["PointId"] = _context.Points.ToList();
            return View();
        }


        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order, string StartPointString, string EndPointString)
        {
            if (ModelState.IsValid)
            {
                // get id of points from string
                var fromPoint = _context.Points.Where(p => p.Name.Replace(" ", "").Contains(StartPointString.Replace(" ", ""))).FirstOrDefault();
                var toPoint = _context.Points.Where(p => p.Name.Replace(" ", "").Contains(EndPointString.Replace(" ", ""))).FirstOrDefault();
                order.StartPointId =  fromPoint != null ? fromPoint.Id : 1;
                order.EndPointId =  toPoint != null ? toPoint.Id : 1;

                // get application user id from identity id
                var identityUserId = UserManager.GetUserId(User);
                var user = _context.ApplicationUsers.Where(p => p.Id == identityUserId).FirstOrDefault();
                if (user == null)
                {
                    return RedirectToAction("Login", "Account", new { Area = "Identity" });
                }
                // get car id from application user id
                var carOfUser = _context.Cars.Where(p => p.UserId == user.AppUserId).FirstOrDefault();
                order.CarId = carOfUser.Id;

                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PointId"] = _context.Points.ToList();

            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["PointId"] = _context.Points.ToList();

            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PointId"] = _context.Points.ToList();

            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Car)
                .Include(o => o.EndPoint)
                .Include(o => o.StartPoint)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
