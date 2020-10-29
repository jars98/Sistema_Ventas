using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistVentas_Web.Models.Almacen.Producto;
using SistVentas_Web.Models.Producto;
using Ventas_Datos;
using Ventas_Entidades.Almacen;

namespace SistVentas_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public ProductosController(DbContextSistema context)
        {
            _context = context;
        }
       //GET: api/Productos/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ProductoViewModel>> Listar()
        {
            var producto = await _context.Productos.Include(p => p.categoria).ToListAsync();

            return producto.Select(p => new ProductoViewModel
            {
                id_producto = p.id_producto,
                id_categoria = p.id_categoria,
                categoria = p.categoria.Nombre,
                codigo = p.codigo,
                nombre = p.nombre,
                precio_venta = p.precio_venta,
                stock = p.stock,
                descripcion = p.descripcion,
                estado = p.estado
            });

        }

        // GET: api/Productos/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var producto = await _context.Productos.Include(a => a.categoria).SingleOrDefaultAsync(a => a.id_producto == id);

            if (producto == null)
            {
                return NotFound();
            }

            return Ok(new ProductoViewModel
            {
                id_producto = producto.id_producto,
                id_categoria = producto.id_categoria,
                categoria = producto.categoria.Nombre,
                codigo = producto.codigo,
                nombre = producto.nombre,
                descripcion = producto.descripcion,
                stock = producto.stock,
                precio_venta = producto.precio_venta,
                estado = producto.estado
            });
        }

        // PUT: api/Productos/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] Models.Almacen.Producto.ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.id_producto <= 0)
            {
                return BadRequest();
            }

            var producto = await _context.Productos.FirstOrDefaultAsync(c => c.id_producto == model.id_producto);

            if (producto == null)
            {
                return NotFound();
            }

            producto.id_categoria = model.id_categoria;
            producto.nombre = model.nombre;
            producto.codigo = model.codigo;
            producto.precio_venta = model.precio_venta;
            producto.stock = model.stock;
            producto.descripcion = model.descripcion;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }

        //POST: api/Productos/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Producto producto = new Producto
            {
                id_categoria = model.id_categoria,
                codigo = model.codigo,
                nombre=model.nombre,
                precio_venta=model.precio_venta,
                stock=model.stock,
                descripcion=model.descripcion,
                estado = true
            };

            _context.Productos.Add(producto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Productos/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var producto = await _context.Productos.FirstOrDefaultAsync(c => c.id_producto == id);

            if (producto == null)
            {
                return NotFound();
            }

            producto.estado = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Categorias/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var producto = await _context.Productos.FirstOrDefaultAsync(c => c.id_producto == id);

            if (producto == null)
            {
                return NotFound();
            }

            producto.estado = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }
        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.id_producto == id);
        }
    }
}