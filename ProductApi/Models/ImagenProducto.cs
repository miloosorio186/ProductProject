using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductApi.Models
{
    public class ImagenProducto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Url { get; set; } = string.Empty;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [ForeignKey("Producto")]
        public int ProductoId { get; set; }
        public Producto? Producto { get; set; }
    }
}
