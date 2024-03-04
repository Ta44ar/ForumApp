using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ForumApp.Data;
using Microsoft.AspNetCore.Identity;
using ForumApp.Models;

namespace ForumApp.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userManager.FindByIdAsync(id).Result;

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}
