using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects.Models
{
    public class FUCarRentingSystemContext : DbContext
    {
        public FUCarRentingSystemContext() :this(new())
        { }
        public FUCarRentingSystemContext(DbContextOptions<FUCarRentingSystemContext> options)
            : base(options)
        { }

        private string GetConnectionString()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            return config.GetConnectionString("Database") ?? throw new ArgumentException("Cannot extract connection string form JSON file");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarProducer> CarProducers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CarRental> CarRentals { get; set; }
        public DbSet<Review> Reviews { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().HasOne(x => x.CarProducer).WithMany(x=>x.Cars).HasForeignKey(x=>x.ProducerID);
            modelBuilder.Entity<CarRental>().HasKey(m => new
            {
                m.CarID,
                m.CustomerID,
                m.PickupDate
            });
            modelBuilder.Entity<Review>().HasKey(m => new
            {
                m.CarID,
                m.CustomerID
            });
        }
    }
}
