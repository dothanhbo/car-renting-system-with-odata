using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class CarProducer
    {
        public CarProducer()
        {
            Cars = new HashSet<Car>();
        }

        public int Id { get; set; }
        public string ProducerName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Country { get; set; } = null!;

        public virtual ICollection<Car> Cars { get; set; }
    }
}
