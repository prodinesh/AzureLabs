using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace AzureFunctions
{
    public static class BooksFunction
    {
        [FunctionName("ReadBooks")]
        public static async Task<IActionResult> RunReadBooks(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            SqlConnection conn = GetConnection();
            List<Book> books = new List<Book>();
            string query = "select * from Books";
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            using(SqlDataReader reader = cmd.ExecuteReader())
            { 
               while(reader.Read())
                {
                    Book book = new Book();
                    book.BookId = reader.GetInt32(0);
                    book.BookIdGuid = reader.GetGuid(1);
                    book.Title = reader.GetString(2);
                    book.Author = reader.GetString(3);
                    book.Genere = reader.GetString(4);
                    book.IsFiction = reader.GetBoolean(5);
                    book.Cost = reader.GetDecimal(6);
                    book.PublishedDate = reader.GetDateTime(7);
                    book.Cover = (byte[])reader[8];
                    string encodedCoverImage = Convert.ToBase64String((byte[])reader[8]);
                    book.ImageUrl = string.Concat("data:image/jpg;base64,", encodedCoverImage);
                    books.Add(book);
                }
            }
            conn.Close();
            return new OkObjectResult(JsonConvert.SerializeObject(books));
        }

        [FunctionName("ReadBook")]
        public static async Task<IActionResult> RunReadBook(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            int _bookId = Convert.ToInt32(req.Query["bookId"]);
            Book book = new Book();
            SqlConnection conn = GetConnection();
            string query = String.Format("select * from Books where Book_Id = {0}", _bookId);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        book.BookId = reader.GetInt32(0);
                        book.BookIdGuid = reader.GetGuid(1);
                        book.Title = reader.GetString(2);
                        book.Author = reader.GetString(3);
                        book.Genere = reader.GetString(4);
                        book.IsFiction = reader.GetBoolean(5);
                        book.Cost = reader.GetDecimal(6);
                        book.PublishedDate = reader.GetDateTime(7);
                        book.Cover = (byte[])reader[8];
                        string encodedCoverImage = Convert.ToBase64String((byte[])reader[8]);
                        book.ImageUrl = string.Concat("data:image/jpg;base64", encodedCoverImage);
                }
                }
            conn.Close();
            return new OkObjectResult(book);
        }

        [FunctionName("CreateBook")]
        public static async Task<IActionResult> RunCreateBook(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Book _book = JsonConvert.DeserializeObject<Book>(requestBody);
            SqlConnection _connection = GetConnection();
            string _statement = "INSERT INTO BOOKS VALUES (@paraBookId, @paraBookIdGuid, @paraTitle," +
                " @paraAuthor, @paraGenre, @paraIsFiction, @paraCost, @paraPublishedOn, " +
                " @paraCover)";
            _connection.Open();
            using(SqlCommand _cmd = new SqlCommand(_statement, _connection))
            {
                _cmd.Parameters.Add("@paraBookId", System.Data.SqlDbType.Int).Value = _book.BookId;
                _cmd.Parameters.Add("@paraBookIdGuid", System.Data.SqlDbType.UniqueIdentifier).Value = _book.BookIdGuid;
                _cmd.Parameters.Add("@paraTitle", System.Data.SqlDbType.VarChar).Value = _book.Title;
                _cmd.Parameters.Add("@paraAuthor", System.Data.SqlDbType.VarChar).Value = _book.Author;
                _cmd.Parameters.Add("@paraGenre", System.Data.SqlDbType.VarChar).Value = _book.Genere;
                _cmd.Parameters.Add("@paraIsFiction", System.Data.SqlDbType.Bit).Value = _book.IsFiction;
                _cmd.Parameters.Add("@paraCost", System.Data.SqlDbType.Decimal).Value = _book.Cost;
                _cmd.Parameters.Add("@paraPublishedOn", System.Data.SqlDbType.DateTime).Value = _book.PublishedDate;
                _cmd.Parameters.Add("@paraCover", System.Data.SqlDbType.VarBinary).Value = _book.Cover;
                _cmd.CommandType = CommandType.Text;
                _cmd.ExecuteNonQuery();
            }
            _connection.Close();
            return new OkObjectResult("Book record created");
        }

        private static SqlConnection GetConnection()
        {
            //Connection string from Local DB
            string strConnection = "Server=DINESH-O\\SQLEXPRESS;database=Experts;Trusted_Connection=True;TrustServerCertificate=True";
            
            //Connection string from Azure SQL
            //string strConnection = "Server=tcp:books2023.database.windows.net,1433;Initial Catalog=Experts;Persist Security Info=False;User ID=sqladmin;Password=Azure@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            
            //Connection string from Azure Functions 
            //string strConnection = Environment.GetEnvironmentVariable("SQLAZURECONNSTR_SQLConnection");
            
            return new SqlConnection(strConnection);
        }
    }
}
