using OnlineStoreWorker.Models;

namespace OnlineStoreWorker.Repositories
{
    public interface ICustomerRepository
    {
        void Insert(Customer customer);
    }
}
