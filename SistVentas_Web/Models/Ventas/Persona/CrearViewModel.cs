using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistVentas_Web.Models.Ventas.Persona
{
    public class CrearViewModel
    {
        [Required]
        public string tipo_porveedor { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "El nombre no debe tener menos de 5 caracteres ni mas de 100")]
        public string nombre { get; set; }
        public string tipo_documento { get; set; }
        public string num_documento { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
    }
}
