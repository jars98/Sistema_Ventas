using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SistVentas_Web.Models.Usuarios.Usuario;
using Ventas_Datos;
using Ventas_Entidades.Usuarios;

namespace SistVentas_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly DbContextSistema _context;
        private readonly IConfiguration _config;
        public UsuariosController(DbContextSistema context, IConfiguration config)
        {
            _context = context;
            _config=config;
        }
        // GET: api/Usuarios/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<UsuarioViewModel>> Listar()
        {
            var usuario = await _context.Usuarios.Include(u => u.rol).ToListAsync();

            return usuario.Select(u => new UsuarioViewModel
            {
                id_usuario = u.Id_Usuario,
                id_rol = u.Id_Rol,
                rol = u.rol.Nombre,
                nombre = u.Nombre,
                tipo_documento = u.Tipo_Documento,
                num_documento = u.Num_Documento,
                direccion = u.Direccion,
                telefono = u.Telefono,
                email = u.Email,
                password_hash = u.Password_Hash,
                estado = u.Estado
            });

        }

        // POST: api/Usuarios/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {

            var email = model.email.ToLower();

            if (await _context.Usuarios.AnyAsync(u => u.Email == email))
            {
                return BadRequest("El email ya existe");
            }

            CrearPasswordHash(model.password, out byte[] passwordHash, out byte[] passwordSalt);

            Usuario usuario = new Usuario
            {
                Id_Rol = model.id_rol,
                Nombre = model.nombre,
                Tipo_Documento = model.tipo_documento,
                Num_Documento = model.num_documento,
                Direccion = model.direccion,
                Telefono = model.telefono,
                Email = model.email.ToLower(),
                Password_Hash = passwordHash,
                Password_Salt = passwordSalt,
                Estado = true
            };

            _context.Usuarios.Add(usuario);
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


        // PUT: api/Articulos/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarUsuViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.id_usuario <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id_Usuario == model.id_usuario);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Id_Rol = model.id_rol;
            usuario.Nombre = model.nombre;
            usuario.Tipo_Documento = model.tipo_documento;
            usuario.Num_Documento = model.num_documento;
            usuario.Direccion = model.direccion;
            usuario.Telefono = model.telefono;
            usuario.Email = model.email.ToLower();

            if (model.update_pass == true)
            {
                CrearPasswordHash(model.password, out byte[] passwordHash, out byte[] passwordSalt);
                usuario.Password_Hash = passwordHash;
                usuario.Password_Salt = passwordSalt;
            }

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

        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }

        // PUT: api/Usuarios/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id_Usuario == id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Estado = false;

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

        // PUT: api/Usuarios/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id_Usuario == id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Estado = true;

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

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var email = model.email.ToLower();
            var usuario = await _context.Usuarios.Where(u=> u.Estado==true).Include(u => u.rol).FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null)
            {
                return NotFound();
            }
            //valida si el usuario ingresado es null retorna false
            if (!VerificarPasswordHash(model.password, usuario.Password_Hash, usuario.Password_Salt))
            {
                return NotFound();
            }
            var claims = new List<Claim>
            {
                //los claim almacenan informacion del usuario por ejemplo el correo, fecha nacimiento, documento etc.
                new Claim(ClaimTypes.NameIdentifier,usuario.Id_Usuario.ToString()),
                new Claim(ClaimTypes.Email,email),
                //con el rol se restringira a que metodo de un controlador tendra acceso cierto usuario segun su rol
                new Claim(ClaimTypes.Role,usuario.rol.Nombre),
                new Claim("Id_Usuario",usuario.Id_Usuario.ToString()),
                new Claim("Rol",usuario.rol.Nombre),
                new Claim("Nombre",usuario.Nombre)
            };

            return Ok(new { token = GenerarToken(claims) });
        }
        

        //metodo que verifica el password y email del usuario para validar y tenga acceso al sistema
        private bool VerificarPasswordHash(string password, byte[] passwordHashAlmacenado, byte[] passwordSalt)
        {
            //encriptamos el password con la clase cryptography en la variable passwordSalt
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                //crea variable donde guarda el password anteriormente encriptado en variable passwordHashNuevo
                var passwordHashNuevo = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                //Comparamos el passwordHashAlmacenado con el passwordHashNuevo si coinciden retorna un true si no   false
                return new ReadOnlySpan<byte>(passwordHashAlmacenado).SequenceEqual(new ReadOnlySpan<byte>(passwordHashNuevo));
            }
        }

        //metodo que genera el token y se lo pasa al usuario al loguearse
        private string GenerarToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds,
                claims: claims);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id_Usuario == id);
        }
    }
}