using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public virtual DbSet<UserEntity> Users { get; set; } = null!;
        public virtual DbSet<UserRoleEntity> UserRoles { get; set; } = null!;
        public virtual DbSet<CustomerEntity> Customers { get; set; } = null!;
        public virtual DbSet<CustomerTypeEntity> CustomerTypes { get; set; } = null!;
        public virtual DbSet<CustomerContactEntity> CustomerContacts { get; set; } = null!;
        public virtual DbSet<ProjectEntity> Projects { get; set; } = null!;
        public virtual DbSet<ServiceEntity> Services { get; set; } = null!;
        public virtual DbSet<StatusTypeEntity> StatusTypes { get; set; } = null!;
        public virtual DbSet<UnitEntity> Units { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd()
                      .HasAnnotation("SqlServer:Identity", "1, 1");
            });

            modelBuilder.Entity<UserRoleEntity>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd()
                      .HasAnnotation("SqlServer:Identity", "1, 1");
            });

            modelBuilder.Entity<CustomerEntity>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd()
                      .HasAnnotation("SqlServer:Identity", "1, 1");
            });

            modelBuilder.Entity<CustomerTypeEntity>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd()
                      .HasAnnotation("SqlServer:Identity", "1, 1");
            });

            modelBuilder.Entity<CustomerContactEntity>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd()
                      .HasAnnotation("SqlServer:Identity", "1, 1");
            });

            modelBuilder.Entity<ProjectEntity>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd()
                      .HasAnnotation("SqlServer:Identity", "101, 1");
            });

            modelBuilder.Entity<ServiceEntity>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd()
                      .HasAnnotation("SqlServer:Identity", "1, 1");
            });

            modelBuilder.Entity<StatusTypeEntity>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd()
                      .HasAnnotation("SqlServer:Identity", "1, 1");
            });

            modelBuilder.Entity<UnitEntity>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd()
                      .HasAnnotation("SqlServer:Identity", "1, 1");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}

