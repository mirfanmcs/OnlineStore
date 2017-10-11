using System.Collections.Generic;
using Dapper;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using OnlineStoreWebApi.Models;
using OnlineStoreWebApi.Settings;

namespace OnlineStoreWebApi.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        readonly IOptions<OnlineStoreDbSettings> _onlineStoreDbSettings;
        public CustomerRepository(IOptions<OnlineStoreDbSettings> onlineStoreDbSettings)
        {
            _onlineStoreDbSettings = onlineStoreDbSettings;
        }

        public IList<Customer> GetAll()
        {
            MySqlConnection connection = new MySqlConnection(_onlineStoreDbSettings.Value.ConnectionString);

            var registeredCustomers = connection.Query<Customer>("Select * from Customers");
            return registeredCustomers.AsList();
        }  
    }
}
