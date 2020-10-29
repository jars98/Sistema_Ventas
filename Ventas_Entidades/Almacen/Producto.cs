using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ventas_Entidades.Almacen
{
    public class Producto
    {
        public int id_producto { get; set; }
        [Required]
        public int id_categoria{get;set;}
        public string codigo { get; set; }
        [StringLength(50,MinimumLength =3,ErrorMessage ="El nombre debe tener mas 3 caracteres y menos de 50 caracteres")]
        public string nombre { get; set; }
        [Required]
        public decimal precio_venta { get; set; }
        [Required]
        public int stock { get; set; }
        public string descripcion { get; set; }
        public bool estado { get; set; }
        public Categoria categoria { get; set; }
    }
}
