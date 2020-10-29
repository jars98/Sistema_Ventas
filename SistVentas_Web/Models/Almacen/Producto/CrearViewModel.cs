using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistVentas_Web.Models.Almacen.Producto
{
    public class CrearViewModel
    {
        [Required]
        public int id_categoria { get; set; }
        public string codigo { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El Nombre debe contener mas de 10 caracteres y menos de 50 caracteres.!!")]
        public string nombre { get; set; }
        [Required]
        public decimal precio_venta { get; set; }
        [Required]
        public int stock { get; set; }
        public string descripcion { get; set; }
    }
}
