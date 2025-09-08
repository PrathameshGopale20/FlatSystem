using FlatSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FlatSystem.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }

        public DbSet<Roles> Roles { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Apartments> Apartments { get; set; }
        public DbSet<Flats> Flats { get; set; }
        public DbSet<Owners> Owners { get; set; }
        public DbSet<Tenants> Tenants { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<SecurityGuard> SecurityGuards { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Role
            modelBuilder.Entity<Roles>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<Roles>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // User
            modelBuilder.Entity<Users>()
            .HasKey(u => u.Id);

            modelBuilder.Entity<Users>()
                .HasOne(u => u.Owner)
                .WithOne(o => o.User)
                .HasForeignKey<Owners>(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Users>()
                .HasOne(u => u.SecurityGuard)
                .WithOne(sg => sg.User)
                .HasForeignKey<SecurityGuard>(sg => sg.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Apartment
            modelBuilder.Entity<Apartments>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Apartments>()
                .HasMany(a => a.Flats)
                .WithOne(f => f.Apartment)
                .HasForeignKey(f => f.ApartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Flat
            modelBuilder.Entity<Flats>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<Flats>()
                .HasOne(f => f.Owner)
                .WithOne(o => o.Flat)
                .HasForeignKey<Owners>(o => o.FlatId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Flats>()
                .HasMany(f => f.Guests)
                .WithOne(g => g.VisitingFlat)
                .HasForeignKey(g => g.VisitingFlatId)
                .OnDelete(DeleteBehavior.Cascade);

            // Owner
            modelBuilder.Entity<Owners>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<Owners>()
                .HasMany(o => o.Tenants)
                .WithOne(t => t.Owner)
                .HasForeignKey(t => t.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Tenant
            modelBuilder.Entity<Tenants>()
                .HasKey(t => t.Id);

            // Guest
            modelBuilder.Entity<Guest>()
                .HasKey(g => g.Id);

            // SecurityGuard
            modelBuilder.Entity<SecurityGuard>()
                .HasKey(sg => sg.Id);

            modelBuilder.Entity<SecurityGuard>()
                .HasOne(sg => sg.AssignedApartment)
                .WithMany()
                .HasForeignKey(sg => sg.AssignedApartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // AuditLog
            modelBuilder.Entity<AuditLog>()
                .HasKey(al => al.Id);

            modelBuilder.Entity<AuditLog>()
                .HasOne(al => al.User)
                .WithMany()
                .HasForeignKey(al => al.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Roles>().HasData(
                new Roles
                {
                    Id = 1,
                    RoleName = "SuperAdmin",

                },
                new Roles
                {
                    Id = 2,
                    RoleName = "Secretary",

                },
                new Roles
                {
                    Id = 3,
                    RoleName = "Owner",

                },
                new Roles
                {
                    Id = 4,
                    RoleName = "Security Guard",

                });
        }

    }

}
