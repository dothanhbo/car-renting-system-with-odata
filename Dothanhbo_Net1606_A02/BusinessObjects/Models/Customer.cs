using BusinessObjects.Models.Base;

namespace BusinessObjects.Models
{
    public class Customer: BaseEntity
    {
        public string CustomerName { get; set; } = default!;
        public string Mobile { get; set; } = default!;
        public DateTime Birthday { get; set; }
        public string IdentityCard { get; set; } = default!;
        public string LicenceNumber { get; set; } = default!;
        public DateTime LicenceDate { get; set; }
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}