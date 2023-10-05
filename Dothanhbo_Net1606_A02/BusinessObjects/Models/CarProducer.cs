using BusinessObjects.Models.Base;
using System.Text;

namespace BusinessObjects.Models
{
    public class CarProducer: BaseEntity
    {
        public string ProducerName { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Country { get; set; } = default!;
        public virtual ICollection<Car> Cars { get; set; } = default!;
    }
}