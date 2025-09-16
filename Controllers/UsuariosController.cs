using Microsoft.AspNetCore.Mvc;
using myApp.Data;
using myApp.Models;

namespace myApp.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly AplicationDbContext _context;

        public UsuariosController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public IActionResult Index()
        {
            var usuarios = _context.Usuarios.ToList();
            return View(usuarios);
        }

        // GET: Crear usuario
        public IActionResult Create()
        {
            return View();
        }

        // POST: Crear usuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Editar usuario
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var usuario = _context.Usuarios.Find(id);
            if (usuario == null) return NotFound();

            return View(usuario);
        }

        // POST: Editar usuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Usuarios.Update(usuario);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Eliminar usuario
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var usuario = _context.Usuarios.Find(id);
            if (usuario == null) return NotFound();

            return View(usuario);
        }

        // POST: Confirmar eliminación
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            // Validar si el usuario tiene préstamos asociados
            bool tienePrestamos = _context.Prestamos.Any(p => p.Usuario.Id == id);
            if (tienePrestamos)
            {
                TempData["Error"] = "No se puede eliminar el usuario porque tiene préstamos asociados.";
                return RedirectToAction("Index");
            }

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Detalles
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var usuario = _context.Usuarios.Find(id);
            if (usuario == null) return NotFound();

            return View(usuario);
        }
    }
}
