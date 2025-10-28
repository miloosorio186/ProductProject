using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Models;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagenesProductosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ImagenesProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ImagenesProductos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImagenProducto>>> GetImagenesProductos()
        {
            return await _context.ImagenesProductos
                                 .Include(i => i.Producto)
                                 .ToListAsync();
        }

        //  GET: api/ImagenesProductos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImagenProducto>> GetImagenProducto(int id)
        {
            var imagen = await _context.ImagenesProductos
                                       .Include(i => i.Producto)
                                       .FirstOrDefaultAsync(i => i.Id == id);

            if (imagen == null)
                return NotFound();

            return imagen;
        }

        //  POST: api/ImagenesProductos
        [HttpPost]
        public async Task<ActionResult<ImagenProducto>> PostImagen(ImagenProducto imagen)
        {
            // Verificar que el producto exista
            var producto = await _context.Productos.FindAsync(imagen.ProductoId);
            if (producto == null)
                return BadRequest($"El producto con ID {imagen.ProductoId} no existe.");

            _context.ImagenesProductos.Add(imagen);
            await _context.SaveChangesAsync();

            // Corregido: el nombre correcto del método es GetImagenProducto
            return CreatedAtAction(nameof(GetImagenProducto), new { id = imagen.Id }, imagen);
        }

        //  PUT: api/ImagenesProductos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImagenProducto(int id, ImagenProducto imagen)
        {
            if (id != imagen.Id)
                return BadRequest();

            _context.Entry(imagen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.ImagenesProductos.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/ImagenesProductos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImagenProducto(int id)
        {
            var imagen = await _context.ImagenesProductos.FindAsync(id);
            if (imagen == null)
                return NotFound();

            _context.ImagenesProductos.Remove(imagen);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
