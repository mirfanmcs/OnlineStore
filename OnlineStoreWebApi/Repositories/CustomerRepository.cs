using System;
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
            var onlineStoreDbUserName = Environment.GetEnvironmentVariable("ONLINE_STORE_DB_USERNAME");
            var onlineStoreDbPassword = Environment.GetEnvironmentVariable("ONLINE_STORE_DB_PASSWORD");

            var connectionString = $"Server={_onlineStoreDbSettings.Value.Server};Database={_onlineStoreDbSettings.Value.Database};Uid={onlineStoreDbUserName};Pwd={onlineStoreDbPassword};";

            MySqlConnection connection = new MySqlConnection(connectionString);

            var registeredCustomers = connection.Query<Customer>("Select * from Customers");
            return registeredCustomers.AsList();
        }  
    }
}
