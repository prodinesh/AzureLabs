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
        public string Title { get; set; }
        public string Genere { get; set; }
        public string Author { get; set; }
        public bool IsFiction { get; set; }
        public decimal Cost { get; set; }
        public DateTime PublishedDate { get; set; }
        public string ImageUrl { get; set; }
        public byte[] Cover { get; set; }
    }
}
