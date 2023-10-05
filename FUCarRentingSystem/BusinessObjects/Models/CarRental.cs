using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BusinessObjects.Models
{
    public partial class CarRental
    {

        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public DateTime PickupDate { get; set; }
        [JsonIgnore]
        public string? FormattedPickupDate => PickupDate.ToString("MM-dd-yyyy");
        public DateTime ReturnDate { get; set; }
        [JsonIgnore]
        public string? FormattedReturnDate => ReturnDate.ToString("MM-dd-yyyy");
        public decimal RentPrice { get; set; }
        public int Status { get; set; }
        public virtual Car Car { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
    }
}
