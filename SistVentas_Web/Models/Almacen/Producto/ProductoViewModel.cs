using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistVentas_Web.Models.Producto
{
    public class ProductoViewModel
    {
        public int id_producto { get; set; }
        public int id_categoria { get; set; }
        public string categoria { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public decimal precio_venta { get; set; }
        public int stock { get; set; }
        public string descripcion { get; set; }
        public bool estado { get; set; }
    }
}
