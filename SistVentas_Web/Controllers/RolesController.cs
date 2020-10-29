using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistVentas_Web.Models.Usuarios.Rol;
using Ventas_Datos;
using Ventas_Entidades.Usuarios;

namespace SistVentas_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public RolesController(DbContextSistema context)
        {
            _context = context;
        }
        // GET: api/Roles/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<RolViewModel>> Listar()
        {
            var rol = await _context.Roles.ToListAsync();

            return rol.Select(r => new RolViewModel
            {
                id_rol  = r.Id_Rol,
                nombre = r.Nombre,
                descripcion = r.Descripcion,
                estado = r.Estado
            });

        }
        // GET: api/Roles/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var rol = await _context.Roles.Where(c => c.Estado == true).ToListAsync();

            return rol.Select(r => new SelectViewModel
            {
                id_rol = r.Id_Rol,
                nombre = r.Nombre
            });

        }

        private bool RolExists(int id)
        {
            return _context.Roles.Any(e => e.Id_Rol == id);
        }
    }
}