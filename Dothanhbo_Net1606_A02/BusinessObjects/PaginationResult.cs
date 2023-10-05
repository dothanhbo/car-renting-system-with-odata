using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class PaginationResult<T> where T : class
    {
        public List<T> Result { get; set; } = new List<T>();
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
        public PaginationResult() { }
    }
}
