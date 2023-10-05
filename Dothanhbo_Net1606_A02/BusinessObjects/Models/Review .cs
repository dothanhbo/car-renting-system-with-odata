using BusinessObjects.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class Review
    {
        public int CustomerID { get; set; }
        public int CarID { get; set; }
        public int ReviewStar { get; set; }
        public string Comment { get; set; } = default!;
        public virtual Car Car { get; set; } = default!;
        public virtual Customer Customer { get; set; } = default!;
    }
}
