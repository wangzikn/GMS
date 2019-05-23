using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GMS.Models;

namespace GMS.Data
{
    public class ApplicationDbContext : IdentityDbContext<Individual>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public  DbSet<AreaConfig> AreaConfig { get; set; }
        public DbSet<Area> Area { get; set; }
        public  DbSet<AccessFuncRoleOrg> AccessFuncRoleOrg { get; set; }        
        public DbSet<Role> Role { get; set; }
        public DbSet<TreeCatalog> TreeCatalog { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AccessFuncRoleOrg>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.FunctionName });

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(200);

                entity.Property(e => e.FunctionName).HasMaxLength(150);

                entity.HasOne(d => d.User)               
                    .WithMany(p => p.AccessFuncRoleOrg)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccessFuncRoleOrg_Individual");
            });
            modelBuilder.Entity<Area>(entity =>
            {
                entity.Property(e => e.District).HasMaxLength(100);

                entity.Property(e => e.Province).HasMaxLength(100);

                entity.Property(e => e.Ward).HasMaxLength(100);
                
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => new { e.UserId });
                entity.Property(e => e.UserRole);
                entity.Property(e => e.TreeCatalogRole);
                entity.Property(e => e.DecentralizationRole);
            });
            modelBuilder.Entity<AreaConfig>(entity =>
            {
                entity.HasKey(e => new { e.Id });
                entity.Property(e => e.Key);
                entity.Property(e => e.Value);
                
            });
            modelBuilder.Entity<TreeCatalog>(entity =>
            {
                entity.HasKey(e => new { e.Id });
                entity.Property(e => e.Name).HasMaxLength(100); ;
                entity.Property(e => e.ScientificName).HasMaxLength(200); ;
                entity.Property(e => e.Description).HasMaxLength(5000); ;
                entity.Property(e => e.Url).HasMaxLength(200); ;
            });
        }
    }
}
