using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductApi.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        // Relación: un producto puede tener muchas imágenes
        public ICollection<ImagenProducto>? Imagenes { get; set; }
    }
}
