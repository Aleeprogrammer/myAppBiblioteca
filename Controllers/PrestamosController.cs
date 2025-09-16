using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myApp.Data;
using myApp.Models;

namespace myApp.Controllers
{
    public class PrestamosController : Controller
    {
        private readonly AplicationDbContext _context;

        public PrestamosController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: Prestamos
        public IActionResult Index()
        {
            var prestamos = _context.Prestamos
                .Include(p => p.Libro)
                .Include(p => p.Usuario)
                .ToList();

            return View(prestamos);
        }

        // GET: Prestamos/Details/5
        public IActionResult Details(int id)
        {
            var prestamo = _context.Prestamos
                .Include(p => p.Libro)
                .Include(p => p.Usuario)
                .FirstOrDefault(p => p.Id == id);

            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        // GET: Prestamos/Create
        public IActionResult Create()
        {
            ViewBag.Libros = _context.Libros.ToList();
            ViewBag.Usuarios = _context.Usuarios.ToList();
            return View();
        }

        // POST: Prestamos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Prestamo prestamo, int libroId, int usuarioId)
        {
            // Asigna los objetos ANTES de validar el modelo
            prestamo.Libro = _context.Libros.FirstOrDefault(l => l.Id == libroId);
            prestamo.Usuario = _context.Usuarios.FirstOrDefault(u => u.Id == usuarioId);

            // Elimina los errores automáticos de las propiedades de navegación
            ModelState.Remove(nameof(prestamo.Libro));
            ModelState.Remove(nameof(prestamo.Usuario));

            // Ahora valida el modelo
            if (ModelState.IsValid && prestamo.Libro != null && prestamo.Usuario != null)
            {
                _context.Prestamos.Add(prestamo);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // Si faltan libro o usuario, agrega errores personalizados
            if (prestamo.Libro == null)
                ModelState.AddModelError(nameof(prestamo.Libro), "Debes seleccionar un libro válido.");
            if (prestamo.Usuario == null)
                ModelState.AddModelError(nameof(prestamo.Usuario), "Debes seleccionar un usuario válido.");

            ViewBag.Libros = _context.Libros.ToList();
            ViewBag.Usuarios = _context.Usuarios.ToList();
            return View(prestamo);
        }

        // GET: Prestamos/Edit/5
        public IActionResult Edit(int id)
        {
            var prestamo = _context.Prestamos
                .Include(p => p.Libro)
                .Include(p => p.Usuario)
                .FirstOrDefault(p => p.Id == id);

            if (prestamo == null)
            {
                return NotFound();
            }

            ViewBag.Libros = _context.Libros.ToList();
            ViewBag.Usuarios = _context.Usuarios.ToList();

            return View(prestamo);
        }

        // POST: Prestamos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Prestamo prestamo, int libroId, int usuarioId)
        {
            var prestamoExistente = _context.Prestamos
                .Include(p => p.Libro)
                .Include(p => p.Usuario)
                .FirstOrDefault(p => p.Id == id);

            if (prestamoExistente == null)
            {
                return NotFound();
            }

            prestamoExistente.FechaPrestamo = prestamo.FechaPrestamo;
            prestamoExistente.FechaDevolucion = prestamo.FechaDevolucion;
            prestamoExistente.Libro = _context.Libros.FirstOrDefault(l => l.Id == libroId);
            prestamoExistente.Usuario = _context.Usuarios.FirstOrDefault(u => u.Id == usuarioId);

            // Elimina los errores automáticos de las propiedades de navegación
            ModelState.Remove(nameof(prestamo.Libro));
            ModelState.Remove(nameof(prestamo.Usuario));

            if (ModelState.IsValid && prestamoExistente.Libro != null && prestamoExistente.Usuario != null)
            {
                _context.Update(prestamoExistente);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            if (prestamoExistente.Libro == null)
                ModelState.AddModelError(nameof(prestamoExistente.Libro), "Debes seleccionar un libro válido.");
            if (prestamoExistente.Usuario == null)
                ModelState.AddModelError(nameof(prestamoExistente.Usuario), "Debes seleccionar un usuario válido.");

            ViewBag.Libros = _context.Libros.ToList();
            ViewBag.Usuarios = _context.Usuarios.ToList();

            return View(prestamoExistente);
        }

        // GET: Prestamos/Delete/5
        public IActionResult Delete(int id)
        {
            var prestamo = _context.Prestamos
                .Include(p => p.Libro)
                .Include(p => p.Usuario)
                .FirstOrDefault(p => p.Id == id);

            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        // POST: Prestamos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var prestamo = _context.Prestamos.FirstOrDefault(p => p.Id == id);
            if (prestamo == null)
            {
                return NotFound();
            }

            _context.Prestamos.Remove(prestamo);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
