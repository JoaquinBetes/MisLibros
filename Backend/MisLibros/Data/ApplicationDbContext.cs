using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MisLibros.Models.Entities;

namespace MisLibros.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Escritor> Escritores { get; set; }
        public DbSet<Editorial> Editoriales { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Apunte> Apuntes { get; set; }
        public DbSet<Biblioteca> Bibliotecas { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Lectura> Lecturas { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<ComentarioArticulo> Comentarios { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Importante para la configuración de Identity

            // Configuración de la entidad Biblioteca
            modelBuilder.Entity<Biblioteca>(entity =>
            {
                // Definir clave primaria compuesta
                entity.HasKey(b => new { b.IdLibro, b.IdEditorial });

                // Relación con Libro
                entity.HasOne(b => b.Libro)
                    .WithMany(l => l.Biblioteca)
                    .HasForeignKey(b => b.IdLibro);
                    //.OnDelete(DeleteBehavior.Cascade);

                // Relación con Editorial
                entity.HasOne(b => b.Editorial)
                    .WithMany(e => e.Biblioteca)
                    .HasForeignKey(b => b.IdEditorial);
                    //.OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Articulo>(entity =>
            {
                // Definir clave primaria compuesta
                entity.HasKey(b => new { b.IdUsuario, b.IdLibro, b.TituloApunte });
                // Relación Articulo -> Usuario
                entity.HasOne(a => a.Usuario)
                .WithMany(u => u.Articulos)
                .HasForeignKey(a => a.IdUsuario);
                // Relación Articulo -> Apunte (clave compuesta)
                entity.HasOne(a => a.Apunte)
                .WithMany(u => u.Articulos)
                .HasForeignKey(a => new { a.IdLibro, a.TituloApunte });
            });

            modelBuilder.Entity<Lectura>(entity =>
            {
                entity.HasKey(b => new { b.IdUsuario, b.IdLibro, b.IdEditorial });

                entity.HasOne(a => a.Usuario)
                .WithMany(u => u.Lecturas)
                .HasForeignKey(a => a.IdUsuario);

                entity.HasOne(a => a.Biblioteca)
                .WithMany(u => u.Lecturas)
                .HasForeignKey(a => new {a.IdLibro, a.IdEditorial});

            });

            modelBuilder.Entity<Cita>(entity =>
            {
                entity.HasKey(entity => entity.Id);

                entity.HasOne(c => c.Biblioteca)
                .WithMany(b => b.Citas)
                .HasForeignKey(c => new { c.IdLibro, c.IdEditorial });

            });

            modelBuilder.Entity<CitaArticulo>(entity =>
            {
                // Clave primaria compuesta NO AGRUPADA
                entity.HasKey(ca => new { ca.CitaId, ca.ArticuloIdUsuario, ca.ArticuloIdLibro, ca.ArticuloTituloApunte })
                      .IsClustered(false); // Índice no agrupado

                // Relación con Cita (sin CASCADE)
                entity.HasOne(ca => ca.Cita)
                      .WithMany(c => c.CitaArticulos)
                      .HasForeignKey(ca => ca.CitaId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relación con Articulo (sin CASCADE)
                entity.HasOne(ca => ca.Articulo)
                      .WithMany(a => a.CitaArticulos)
                      .HasForeignKey(ca => new { ca.ArticuloIdUsuario, ca.ArticuloIdLibro, ca.ArticuloTituloApunte })
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ComentarioArticulo>(entity =>
            {
                entity.HasKey(k => new 
                { k.IdUsuarioComentario, k.IdUsuarioArticulo, k.IdLibro, k.TituloApunte })
                .IsClustered(false);

                entity.HasOne(c => c.Usuario)
                    .WithMany(u => u.Comentarios)
                    .HasForeignKey(c => c.IdUsuarioComentario)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Articulo)
                    .WithMany(a => a.Comentarios)
                    .HasForeignKey(c => new { c.IdUsuarioArticulo, c.IdLibro, c.TituloApunte })
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Like>(entity =>
            {
                entity.HasKey(k => new
                { k.IdUsuarioComentario, k.IdUsuarioArticulo, k.IdLibro, k.TituloApunte })
                .IsClustered(false);

                entity.HasOne(c => c.Usuario)
                    .WithMany()
                    .HasForeignKey(c => c.IdUsuarioComentario)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Articulo)
                    .WithMany()
                    .HasForeignKey(c => new { c.IdUsuarioArticulo, c.IdLibro, c.TituloApunte })
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
