using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BusinessObjects.Models
{
    public partial class Customer
    {
        public Customer()
        {
            CarRentals = new HashSet<CarRental>();
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public string CustomerName { get; set; } = null!;
        public string Mobile { get; set; } = null!;
        public DateTime Birthday { get; set; }
        [JsonIgnore]
        public string? FormattedBirthday => Birthday.ToString("MM-dd-yyyy");
        public string IdentityCard { get; set; } = null!;
        public string LicenceNumber { get; set; } = null!;
        public DateTime LicenceDate { get; set; }
        [JsonIgnore]
        public string? FormattedLicenceDate => LicenceDate.ToString("MM-dd-yyyy");
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<CarRental> CarRentals { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
