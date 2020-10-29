using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistVentas_Web.Controllers
{
    public class ActualizarViewModel
    {
        [Required]
        public int Id_Categoria { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El Nombre debe contener mas de 10 caracteres y menos de 50 caracteres.!!")]
        public string Nombre { get; set; }
        [StringLength(256)]
        public string Descripcion { get; set; }
    }
}
