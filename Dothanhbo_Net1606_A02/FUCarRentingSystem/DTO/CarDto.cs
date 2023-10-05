using BusinessObjects.Models;

namespace FUCarRentingSystem.DTO
{
    public class CarDto
    {
        public string CarName { get; set; } = default!;
        public int CarModelYear { get; set; }
        public string Color { get; set; } = default!;
        public string Capacity { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime ImportDate { get; set; }
        public int ProducerID { get; set; } = default!;
        public decimal RentPrice { get; set; }
        public int Status { get; set; }
    }
}
