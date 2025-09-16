namespace myApp.Models
{
    public class Prestamo
    {
        public int Id { get; set; }
        
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaDevolucion { get; set; }

        // Navegación
        public Libro Libro { get; set; }
        public Usuario Usuario { get; set; }
    }
}
