using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;

namespace SportStoreWiki.Models;

public partial class SportStoreContext : DbContext
{
    public SportStoreContext()
    {
    }

    public SportStoreContext(DbContextOptions<SportStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderProduct> OrderProducts { get; set; }

    public virtual DbSet<PickupPoint> PickupPoints { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["ConnectionLocalDb"].ToString());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07C8B9F3A7");

            entity.ToTable("Order");

            entity.Property(e => e.DeliveryDate).HasColumnType("date");
            entity.Property(e => e.OrderDate).HasColumnType("date");

            entity.HasOne(d => d.PickupPointNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PickupPoint)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__PickupPoi__44FF419A");
        });

        modelBuilder.Entity<OrderProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderPro__3214EC07113575FE");

            entity.ToTable("OrderProduct");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderProducts)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderProd__Order__3C69FB99");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderProd__Produ__4AB81AF0");
        });

        modelBuilder.Entity<PickupPoint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PickupPo__3214EC07E7620A2E");

            entity.ToTable("PickupPoint");

            entity.Property(e => e.NumberStreet).HasColumnName("NUmberStreet");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC077AAD4D65");

            entity.ToTable("Product");

            entity.HasIndex(e => e.ArticleNumber, "UQ__tmp_ms_x__3C991142BA052CBD").IsUnique();

            entity.Property(e => e.ArticleNumber).HasMaxLength(100);
            entity.Property(e => e.Cost).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Photo).IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC078FED1803");

            entity.ToTable("Role");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC0797387191");

            entity.ToTable("User");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Patronymic).HasMaxLength(100);
            entity.Property(e => e.Surname).HasMaxLength(100);

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__Role__267ABA7A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
