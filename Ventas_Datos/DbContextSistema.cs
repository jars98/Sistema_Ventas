using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Ventas_Datos.Mapping.Almacen;
using Ventas_Datos.Mapping.Usuarios;
using Ventas_Datos.Mapping.Ventas;
using Ventas_Entidades.Almacen;
using Ventas_Entidades.Usuarios;
using Ventas_Entidades.Ventas;

namespace Ventas_Datos
{
    public class DbContextSistema:DbContext
    {
        //Creamos una coleccion de las categorias con el nombre Categorias de la entidad Categoria
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        //Constructor de la clase con parametros (Se instancia un objeto de la clase DbContextOptions con el parametro options)
        //que es de tipo DbContextSistema
        public DbContextSistema(DbContextOptions<DbContextSistema>options):base(options)
        {

        }
        //Metodo que permite mapear las entidades de la base de datos y se manda un parametro llamado modelBuilder
        //Que instancia de la clase ModelBuilder
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Se le indica al modelBuilder que aplique la configuracion de las tabla mapeadas de la base de datos
            modelBuilder.ApplyConfiguration(new CategoriasMap());
            modelBuilder.ApplyConfiguration(new ProductoMap());
            modelBuilder.ApplyConfiguration(new RolMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new ProveedorMap());
        }
    }
}
