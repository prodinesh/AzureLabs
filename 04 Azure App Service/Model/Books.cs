namespace AzureSQL_ServiceApp.Model
{
    public class Books
    {
        public int BookId { get; set; }
        public Guid BookIdGuid { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genere { get; set; }
        public bool IsFiction { get; set; }
        public decimal Cost { get; set; }
        public DateTime PublishedDate { get; set; }
        public byte[] Cover { get; set; }
        public string ImageUrl { get; set; }
    }
}
