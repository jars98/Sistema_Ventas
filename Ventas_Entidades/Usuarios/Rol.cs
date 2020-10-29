using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ventas_Entidades.Usuarios
{
    public class Rol
    {
        public int Id_Rol { get; set; }
        [Required]
        [StringLength(30,MinimumLength =3,ErrorMessage ="El nombre no debe tener menos de 5 caracteres ni mas de 20")]
        public string Nombre { get; set; }
        [StringLength(256)]
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public ICollection<Usuario> usuarios { get; set; }
    }
}
