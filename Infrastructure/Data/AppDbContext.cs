using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Role> Roles { get; set; } // Agregar DbSet<Role>
        public DbSet<Entity> Entities { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //// Configuración de la relación muchos a muchos entre ApplicationUser y Role
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity(j => j.ToTable("UserRoles")); // Tabla intermedia

            // Especificar la precisión y escala para el campo Salary de Employee
            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasColumnType("decimal(18,2)"); // Define un tipo de columna decimal con precisión 18 y escala 2

            // Configurar relación uno a muchos entre Entity y Employee
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Entity) // Un empleado tiene una entidad
                .WithMany(ent => ent.Employees) // Una entidad tiene muchos empleados
                .HasForeignKey(e => e.EntityId) // Clave foránea en Employee
                .OnDelete(DeleteBehavior.Cascade); // Comportamiento en cascada al eliminar una entidad
        }
    }
}
