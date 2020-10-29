using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace Ventas_Entidades.Usuarios
{
    public class Usuario
    {
        public int Id_Usuario { get; set; }
        [Required]
        public int Id_Rol { get; set; }
        [Required]
        [StringLength(100,MinimumLength =5,ErrorMessage ="El nombre no debe tener menos de 5 caracteres ni mas de 100")]
        public string Nombre { get; set; }
        public string Tipo_Documento{get;set;}
        public string Num_Documento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public byte [] Password_Hash { get; set; }
        [Required]
        public byte[] Password_Salt { get; set; }
        public bool Estado { get; set; }
        public Rol rol { get; set; }
    }
}
