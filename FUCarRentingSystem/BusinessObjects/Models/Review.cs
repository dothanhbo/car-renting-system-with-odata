using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Review
    {
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public int ReviewStar { get; set; }
        public string Comment { get; set; } = null!;

        public virtual Car Car { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
    }
}
