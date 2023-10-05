using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BusinessObjects.Models
{
    public partial class DoThanhBo_Net1606_A02Context : DbContext
    {
        public DoThanhBo_Net1606_A02Context()
        {
        }

        public DoThanhBo_Net1606_A02Context(DbContextOptions<DoThanhBo_Net1606_A02Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Cars { get; set; } = null!;
        public virtual DbSet<CarProducer> CarProducers { get; set; } = null!;
        public virtual DbSet<CarRental> CarRentals { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(local);Database=DoThanhBo_Net1606_A02;Uid=sa;Pwd=123456;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasIndex(e => e.ProducerId, "IX_Cars_ProducerID");

                entity.Property(e => e.ProducerId).HasColumnName("ProducerID");

                entity.Property(e => e.RentPrice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.CarProducer)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.ProducerId);
            });

            modelBuilder.Entity<CarRental>(entity =>
            {
                entity.HasKey(e => new { e.CarId, e.CustomerId, e.PickupDate });

                entity.HasIndex(e => e.CustomerId, "IX_CarRentals_CustomerID");

                entity.Property(e => e.CarId).HasColumnName("CarID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.RentPrice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.CarRentals)
                    .HasForeignKey(d => d.CarId);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CarRentals)
                    .HasForeignKey(d => d.CustomerId);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Email).HasDefaultValueSql("(N'')");

                entity.Property(e => e.Password).HasDefaultValueSql("(N'')");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => new { e.CarId, e.CustomerId });

                entity.HasIndex(e => e.CustomerId, "IX_Reviews_CustomerID");

                entity.Property(e => e.CarId).HasColumnName("CarID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.CarId);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.CustomerId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
