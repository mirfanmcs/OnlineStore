using Dapper;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using OnlineStoreWorker.Models;
using OnlineStoreWorker.Settings;

namespace OnlineStoreWorker.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        readonly IOptions<OnlineStoreDbSettings> _onlineStoreDbSettings;
        public CustomerRepository(IOptions<OnlineStoreDbSettings> onlineStoreDbSettings)
        {
            _onlineStoreDbSettings = onlineStoreDbSettings;
        }

        public void Insert(Customer customer)
        {
            MySqlConnection connection = new MySqlConnection(_onlineStoreDbSettings.Value.ConnectionString);


            var count = connection.Execute(@"insert into Customers (FirstName, LastName,EmailAddress,NotifyMe) values (@FirstName, @LastName,@EmailAddress,@NotifyMe)",
                                           customer);

        }
         
    }
}
