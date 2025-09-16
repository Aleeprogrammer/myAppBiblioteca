using Microsoft.AspNetCore.Mvc;
using myApp.Data;
using myApp.Models;

namespace myApp.Controllers
{
    public class LibrosController : Controller
    {
        private readonly AplicationDbContext _context;

        public LibrosController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: Libros
        public IActionResult Index()
        {
            var listaLibros = _context.Libros.ToList();
            return View(listaLibros);
        }

        // GET: Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Libro libro)
        {
            if (ModelState.IsValid)
            {
                _context.Libros.Add(libro);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(libro);
        }

        // GET: Edit
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var libro = _context.Libros.Find(id);
            if (libro == null)
                return NotFound();

            return View(libro);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Libro libro)
        {
            if (ModelState.IsValid)
            {
                _context.Libros.Update(libro);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(libro);
        }

        // GET: Delete
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var libro = _context.Libros.Find(id);
            if (libro == null)
                return NotFound();

            return View(libro);
        }

        // POST: DeleteConfirmed
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var libro = _context.Libros.FirstOrDefault(l => l.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            // Validar si el libro tiene préstamos asociados
            bool tienePrestamos = _context.Prestamos.Any(p => p.Libro.Id == id);
            if (tienePrestamos)
            {
                TempData["Error"] = "No se puede eliminar el libro porque tiene préstamos asociados.";
                return RedirectToAction("Index");
            }

            _context.Libros.Remove(libro);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Details
        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            var libro = _context.Libros.Find(id);
            if (libro == null)
                return NotFound();

            return View(libro);
        }
    }
}