using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; } = null!;
    public DbSet<UserRoleEntity> UserRoles { get; set; } = null!;
    public DbSet<CustomerEntity> Customers { get; set; } = null!;
    public DbSet<CustomerContactEntity> CustomerContacts { get; set; } = null!;
    public DbSet<ProjectEntity> Projects { get; set; } = null!;
    public DbSet<ServiceEntity> Services { get; set; } = null!;
    public DbSet<StatusTypeEntity> StatusTypes { get; set; } = null!;
    public DbSet<UnitEntity> Units { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectEntity>(entity =>
        {
            entity.HasOne(p => p.Customer)
                  .WithMany(c => c.Projects)
                  .HasForeignKey(p => p.CustomerId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(p => p.Service)
                    .WithMany(s => s.Projects)
                    .HasForeignKey(p => p.ServiceId)
                    .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(p => p.Status)
                    .WithMany(s => s.Projects)
                    .HasForeignKey(p => p.StatusId)
                    .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(p => p.User)
                    .WithMany(u => u.Projects)
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

            entity.Property(e => e.Id)
                  .ValueGeneratedOnAdd()
                  .HasAnnotation("SqlServer:Identity", "101, 1");
        });

        modelBuilder.Entity<CustomerEntity>(entity =>
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

            entity.HasOne(cc => cc.Customer)
                    .WithMany(c => c.Contacts)
                    .HasForeignKey(cc => cc.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasOne(u => u.Role)
                  .WithMany(ur => ur.Users)
                  .HasForeignKey(u => u.RoleId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.Property(e => e.Id)
                  .ValueGeneratedOnAdd()
                  .HasAnnotation("SqlServer:Identity", "1, 1");
        });

        modelBuilder.Entity<ServiceEntity>(entity =>
        {
            entity.HasOne(s => s.Unit)
                  .WithMany(u => u.Services)
                  .HasForeignKey(s => s.UnitId)
                  .OnDelete(DeleteBehavior.Cascade);

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

