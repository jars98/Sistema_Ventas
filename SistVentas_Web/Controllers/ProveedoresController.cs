using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistVentas_Web.Models.Ventas.Persona;
using Ventas_Datos;
using Ventas_Entidades.Ventas;

namespace SistVentas_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public ProveedoresController(DbContextSistema context)
        {
            _context = context;
        }
        //GET: api/Proveedores/ListarClientes
        [HttpGet("[action]")]
        public async Task<IEnumerable<ProveedorViewModel>> ListarClientes()
        {
            var proveedor = await _context.Proveedores.Where(p=>p.Tipo_Porveedor=="Cliente").ToListAsync();

            return proveedor.Select(p => new ProveedorViewModel
            {
                id_proveedor = p.Id_Proveedor,
                tipo_porveedor = p.Tipo_Porveedor,
                nombre = p.Nombre,
                tipo_documento = p.Tipo_Documento,
                num_documento = p.Num_Documento,
                direccion = p.Direccion,
                telefono = p.Telefono,
                email = p.Email
            });
        }
        //GET: api/Proveedores/ListarProveedores
        [HttpGet("[action]")]
        public async Task<IEnumerable<ProveedorViewModel>> ListarProveedores()
        {
            var proveedor = await _context.Proveedores.Where(p => p.Tipo_Porveedor == "Proveedor").ToListAsync();

            return proveedor.Select(p => new ProveedorViewModel
            {
                id_proveedor = p.Id_Proveedor,
                tipo_porveedor = p.Tipo_Porveedor,
                nombre = p.Nombre,
                tipo_documento = p.Tipo_Documento,
                num_documento = p.Num_Documento,
                direccion = p.Direccion,
                telefono = p.Telefono,
                email = p.Email
            });
        }
        //POST: api/Proveedores/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = model.email.ToLower();

            if(await _context.Proveedores.AnyAsync(p=> p.Email == email))
            {
                return BadRequest("El Email ya existe");
            }

            Proveedor proveedor = new Proveedor
            {
                Tipo_Porveedor = model.tipo_porveedor,
                Nombre = model.nombre,
                Tipo_Documento = model.tipo_documento,
                Num_Documento = model.num_documento,
                Direccion = model.direccion,
                Telefono = model.telefono,
                Email = model.email.ToLower()
            };

            _context.Proveedores.Add(proveedor);
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
        // PUT: api/Proveedores/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] Models.Ventas.Persona.ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.id_proveedor <= 0)
            {
                return BadRequest();
            }

            var proveedor = await _context.Proveedores.FirstOrDefaultAsync(p => p.Id_Proveedor == model.id_proveedor);

            if (proveedor == null)
            {
                return NotFound();
            }

            proveedor.Tipo_Porveedor = model.tipo_porveedor;
            proveedor.Nombre = model.nombre;
            proveedor.Tipo_Documento = model.tipo_documento;
            proveedor.Num_Documento = model.num_documento;
            proveedor.Direccion = model.direccion;
            proveedor.Telefono = model.telefono;
            proveedor.Email = model.email.ToLower();

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
        private bool ProveedorExists(int id)
        {
            return _context.Proveedores.Any(e => e.Id_Proveedor == id);
        }
    }
}