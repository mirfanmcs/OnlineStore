using System.Collections.Generic;
using Dapper;
using MySql.Data.MySqlClient;
using OnlineStoreWebApi.Messaging;
using OnlineStoreWebApi.Models;
using OnlineStoreWebApi.Repositories;

namespace OnlineStoreWebApi.Services
{
    public class CustomerService : ICustomerService
    {

        readonly ICustomerRepository _customerRepository;
        readonly IOnlineStoreMq _onlineStoreMq;

        public CustomerService(ICustomerRepository customerRepository,IOnlineStoreMq onlineStoreMq)
        {
            _customerRepository = customerRepository;
            _onlineStoreMq = onlineStoreMq;
        }

        public IList<Customer> GetRegisteredCustomers()
        {
            return _customerRepository.GetAll();
        }

        public void RegisterCustomer(Customer customer)
        {
            _onlineStoreMq.SendMessage(customer);
        }
    }
}
