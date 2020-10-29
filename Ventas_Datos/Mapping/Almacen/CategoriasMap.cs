using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Ventas_Entidades.Almacen;

namespace Ventas_Datos.Mapping.Almacen
{
    public class CategoriasMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            //Mapeamos la entidad Categorias de la tabla Categoria de la base de datos con su llave primaria 
            builder.ToTable("Categoria")
                 .HasKey(c => c.Id_Categoria);
            builder.Property(c => c.Nombre)
                .HasMaxLength(50);
            builder.Property(c => c.Descripcion)
                .HasMaxLength(256);
        }
    }
}
