using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctions
{
    public class Book
    {
        public int BookId { get; set; }
        public Guid BookIdGuid { get; set; }
        public string BookTitle { get; set; }
        public string Genere { get; set; }
        public string Author { get; set; }
        public bool IsFiction { get; set; }
        public decimal Cost { get; set; }
        public DateTime PublishedOn { get; set; }
    }
}
