using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Ventas_Entidades.Usuarios;

namespace Ventas_Datos.Mapping.Usuarios
{
    public class RolMap : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable("Rol")
                 .HasKey(r => r.Id_Rol);
        }
    }
}
