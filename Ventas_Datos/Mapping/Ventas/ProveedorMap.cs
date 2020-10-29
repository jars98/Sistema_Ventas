using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Ventas_Entidades.Ventas;

namespace Ventas_Datos.Mapping.Ventas
{
    public class ProveedorMap : IEntityTypeConfiguration<Proveedor>
    {
        public void Configure(EntityTypeBuilder<Proveedor> builder)
        {
            builder.ToTable("Proveedor").
                HasKey(p => p.Id_Proveedor);
        }
    }
}
