using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BusinessObjects.Models
{
    public partial class Car
    {
        public Car()
        {
            CarRentals = new HashSet<CarRental>();
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public string CarName { get; set; } = null!;
        public int CarModelYear { get; set; }
        public string Color { get; set; } = null!;
        public string Capacity { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime ImportDate { get; set; }
        [JsonIgnore]
        public string? FormattedImportDate => ImportDate.ToString("MM-dd-yyyy");
        public int ProducerId { get; set; }
        public decimal RentPrice { get; set; }
        public int Status { get; set; }

        public virtual CarProducer CarProducer { get; set; } = null!;
        public virtual ICollection<CarRental> CarRentals { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
