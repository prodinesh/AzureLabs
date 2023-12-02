using AzureSQL_ServiceApp.Interface;
using AzureSQL_ServiceApp.Model;
using Microsoft.Data.SqlClient;

namespace AzureSQL_ServiceApp.DAL
{
    public class BooksService : IBooksService
    {
        public List<Books> GetBooks()
        {
            List<Books> listBooks = new List<Books>();
            string query = "select * from Books";
            SqlConnection conn = new SqlConnection("Server=DINESH-O\\SQLEXPRESS;database=Experts;Trusted_Connection=True;TrustServerCertificate=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Books book = new Books();
                    book.BookId = reader.GetInt32(0);
                    book.GuidId = reader.GetGuid(1);
                    book.Title = reader.GetString(2);
                    book.Author = reader.GetString(3);
                    book.Genere = reader.GetString(4);
                    book.IsFiction = reader.GetBoolean(5);
                    book.Price = reader.GetDecimal(6);
                    book.PublishedDate = reader.GetDateTime(7);
                    listBooks.Add(book);
                }
                
            }
            conn.Close();
            return listBooks;
        }
    }
}
