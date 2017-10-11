using OnlineStoreWebApi.Models;

namespace OnlineStoreWebApi.Messaging
{
    public interface IOnlineStoreMq
    {
        void SendMessage(Customer customer);
    }
}
