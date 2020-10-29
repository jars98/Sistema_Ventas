using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ventas_Entidades.Almacen
{
    public class Categoria
    {
        public int Id_Categoria { get; set; }
        [Required]
        [StringLength(50,MinimumLength =3,ErrorMessage ="El Nombre debe contener mas de 10 caracteres y menos de 50 caracteres.!!")]
        public string Nombre { get; set; }
        [StringLength(256)]
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public ICollection<Producto> productos { get; set; }

    }
}
