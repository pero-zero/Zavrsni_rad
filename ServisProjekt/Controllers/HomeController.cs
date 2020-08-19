using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServisProjekt.Data;
using ServisProjekt.Models;
using ServisProjekt.Services;

namespace ServisProjekt.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var ogranicenje = _context.UserRoles.Where(x => x.UserId == user.Id).FirstOrDefault().RoleId;
            if (_context.Roles.Where(x => x.Id == ogranicenje).FirstOrDefault().Name == "Korisnik")
            {
                return RedirectToAction(nameof(KorisnikController.PopisPrijava), "Korisnik");
            }
            else if (_context.Roles.Where(x => x.Id == ogranicenje).FirstOrDefault().Name == "Serviser")
            {
                return RedirectToAction(nameof(ServiserController.PopisPrijava), "Serviser");
            }
            else if (_context.Roles.Where(x => x.Id == ogranicenje).FirstOrDefault().Name == "Administrator")
            {
                return RedirectToAction(nameof(AdminController.PopisPrijava), "Admin");
            }
            return View("/Views/Shared/AccessDenied");
        }
        
    }
}
