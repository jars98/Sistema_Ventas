using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ventas_Entidades.Ventas
{
    public class Proveedor
    {
        public int Id_Proveedor { get; set; }
        [Required]
        public string Tipo_Porveedor { get; set; }
        [Required]
        [StringLength (100,MinimumLength =5,ErrorMessage ="El nombre no debe tener menos de 5 caracteres ni mas de 100")]
        public string Nombre { get; set; }
        public string Tipo_Documento { get; set; }
        public string Num_Documento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email  { get; set; }
    }
}
