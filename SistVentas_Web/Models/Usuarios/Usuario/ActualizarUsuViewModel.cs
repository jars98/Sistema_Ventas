using System.ComponentModel.DataAnnotations;

namespace SistVentas_Web.Models.Usuarios.Usuario
{
    public class ActualizarUsuViewModel
    {
        public int id_usuario { get; set; }
        [Required]
        public int id_rol { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "El nombre no debe tener menos de 5 caracteres ni mas de 100")]
        public string nombre { get; set; }
        public string tipo_documento { get; set; }
        public string num_documento { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        public bool update_pass { get; set; }
    }
}
