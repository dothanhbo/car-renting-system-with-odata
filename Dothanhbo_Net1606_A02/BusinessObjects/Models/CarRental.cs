using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
    public class CarRental
    {
        [Key]
        public int CustomerID { get; set; }
        [Key]
        public int CarID { get; set; }
        [Key]
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal RentPrice { get; set; }
        public int Status { get; set; }
        public virtual Car Car { get; set; } = default!;
        public virtual Customer Customer { get; set; } = default!;
    }
}