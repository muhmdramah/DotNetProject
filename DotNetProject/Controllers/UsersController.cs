using DotNetProject.Context;
using DotNetProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetProject.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == user.Email);

                if (existingUser == null)
                {
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    // Redirect to Login page after successful registration
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("Email", "Email is already in use.");
                }
            }

            return View(user);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

                if (user != null)
                    // Redirect to the Products page after successful login
                    return RedirectToAction("Index", "Products");
                else
                    ModelState.AddModelError("", "Invalid login attempt.");
            }

            return View();
        }

    }
}