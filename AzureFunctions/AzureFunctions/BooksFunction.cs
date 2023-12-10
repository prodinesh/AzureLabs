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

namespace AzureFunctions
{
    public static class BooksFunction
    {
        [FunctionName("GetBooks")]
        public static async Task<IActionResult> RunGetBooks(
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
                    book.BookTitle = reader.GetString(2);
                    book.Author = reader.GetString(3);
                    book.Genere = reader.GetString(4);
                    book.IsFiction = reader.GetBoolean(5);
                    book.Cost = reader.GetDecimal(6);
                    book.PublishedOn = reader.GetDateTime(7);
                    books.Add(book);
                }
            }
            conn.Close();
            return new OkObjectResult(books);
        }

        [FunctionName("GetBook")]
        public static async Task<IActionResult> RunGetBook(
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
                        book.BookTitle = reader.GetString(2);
                        book.Author = reader.GetString(3);
                        book.Genere = reader.GetString(4);
                        book.IsFiction = reader.GetBoolean(5);
                        book.Cost = reader.GetDecimal(6);
                        book.PublishedOn = reader.GetDateTime(7);
                    }
                }
            conn.Close();
            return new OkObjectResult(book);
        }

        private static SqlConnection GetConnection()
        {
            string strConnection = "Server=DINESH-O\\SQLEXPRESS;database=Experts;Trusted_Connection=True;TrustServerCertificate=True";
            return new SqlConnection(strConnection);
        }
    }
}
