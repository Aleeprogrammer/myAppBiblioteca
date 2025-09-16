using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using myApp.Models;
using myApp.Data;

namespace myApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, AplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.LibrosCount = _context.Libros.Count();
            ViewBag.UsuariosCount = _context.Usuarios.Count();
            ViewBag.PrestamosCount = _context.Prestamos.Count();
            return View();
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



