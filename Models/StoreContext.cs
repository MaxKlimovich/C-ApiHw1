using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace Ex1.Models;

public class StoreContext : DbContext 
{
    public DbSet<Group> Groups { get; set; }
    
    public DbSet<Product> Products { get; set; }
    
    public DbSet<Store> Stores { get; set; }

    public StoreContext()
    {
        
    }
    public StoreContext(DbContextOptions<StoreContext> abc) : base(abc)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.;Database=ProductStore;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");

            entity.HasKey(x => x.Id).HasName("ProductID");
            entity.HasIndex(x => x.Name).IsUnique();

            entity.Property(e => e.Name)
                .HasColumnName("ProductName")
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(e => e.Description)
                .HasColumnName("Description")
                .HasMaxLength(255)
                .IsRequired();
            
            entity.Property(e => e.Price)
                .HasColumnName("Price") 
                .IsRequired();

            entity.HasOne(x => x.Group)
                .WithMany(c => c.Products)
                .HasForeignKey(x => x.Id)
                .HasConstraintName("GroupToProduct");
        });
        
        modelBuilder.Entity<Group>(entity =>
        {
            entity.ToTable("ProductGroups");

            entity.HasKey(x => x.Id).HasName("GroupID");
            entity.HasIndex(x => x.Name).IsUnique();

            entity.Property(e => e.Name)
                .HasColumnName("ProductName")
                .HasMaxLength(255)
                .IsRequired();
        });
        
        modelBuilder.Entity<Store>(entity =>
        {

            entity.ToTable("Storage");

            entity.HasKey(x => x.Id).HasName("StoreID");


            entity.Property(e => e.Name)
                .HasColumnName("StorageName");

            entity.Property(e => e.Count)
                .HasColumnName("ProductCount");

            entity.HasMany(x => x.Products)
                .WithMany(m => m.Stores)
                .UsingEntity(j => j.ToTable("StorageProduct"));
        });
    }
}





