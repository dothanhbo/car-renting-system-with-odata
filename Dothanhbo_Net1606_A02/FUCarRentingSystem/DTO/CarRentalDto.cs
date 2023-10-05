using BusinessObjects.Models;
using System.ComponentModel.DataAnnotations;

namespace FUCarRentingSystem.DTO
{
    public class CarRentalDto
    {
        public int CustomerID { get; set; }
        public int CarID { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal RentPrice { get; set; }
        public int Status { get; set; }
    }
}
