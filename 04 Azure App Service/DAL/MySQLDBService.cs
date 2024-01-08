using AzureSQL_ServiceApp.Interface;
using AzureSQL_ServiceApp.Model;
using Microsoft.FeatureManagement;
using MySql.Data.MySqlClient;
using System.Text.Json;

namespace BookStore_AzureAppService.DAL
{
    public class MySQLDBService: IBooksService
    {

        private readonly IFeatureManager _featureManager;
        private readonly string azureFunctionUrl = "http://localhost:7192/api/ReadBooks";

        public MySQLDBService(IFeatureManager featureManager)
        {
            _featureManager = featureManager;
        }

        public List<Books> GetBooks()
        {
            List<Books> listBooks = new List<Books>();
            var query = "select * from BOOKS;";
            MySqlConnection connstr = GetMySqlConnection();
            connstr.Open();
            MySqlCommand cmd = new MySqlCommand(query, connstr);
            using(MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Books book = new Books();
                    book.BookId = reader.GetInt32(0);
                    book.BookIdGuid = reader.GetGuid(1);
                    book.Title = reader.GetString(2);
                    book.Author = reader.GetString(3);
                    book.Genere = reader.GetString(4);
                    book.IsFiction = reader.GetBoolean(5);
                    book.Cost = reader.GetDecimal(6);
                    book.PublishedDate = reader.GetDateTime(7);
                    string encodedCoverBytes = Convert.ToBase64String((byte[])reader[8]);
                    book.ImageUrl = string.Concat("data:image/jpg;base64,", encodedCoverBytes);
                    listBooks.Add(book);
                }
            }
            connstr.Close();
            return listBooks;
        }


        private MySqlConnection GetMySqlConnection()
        {
            var mySQLConnectionString = "Server=\"localhost\";UserID = \"root\";Password=\"Azure123\";Database=\"experts\";SslMode=Required;SslCa=\"{path_to_CA_cert}\";";
            return new MySqlConnection(mySQLConnectionString);
        }

        public async Task<bool> IsBeta()
        {
            if (await _featureManager.IsEnabledAsync("Beta"))
                return true;
            else
                return false;
        }

        public async Task<List<Books>> GetBooksByAzureFunction()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(azureFunctionUrl);
                string content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Books>>(content);
            }
        }
    }
}
