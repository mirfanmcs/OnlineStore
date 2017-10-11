using System;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OnlineStoreWorker.Models;
using OnlineStoreWorker.Repositories;
using OnlineStoreWorker.Settings;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OnlineStoreWorker.Messaging
{
    public class OnlineStoreMq : IOnlineStoreMq
    {
        readonly IOptions<OnlineStoreMqSettings> _onlineStoreMqSettings;
       
        readonly ICustomerRepository _customerRepository;
        public OnlineStoreMq(IOptions<OnlineStoreMqSettings> onlineStoreMqSettings, ICustomerRepository customerRepository)
        {
            _onlineStoreMqSettings = onlineStoreMqSettings;
            _customerRepository = customerRepository;
        }
       
        public void ConsumeMessage()
        {
            try
            {
                var factory = new ConnectionFactory()
                { HostName = _onlineStoreMqSettings.Value.HostName, UserName = _onlineStoreMqSettings.Value.UserName, Password = _onlineStoreMqSettings.Value.Password };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {

                    channel.ExchangeDeclare(_onlineStoreMqSettings.Value.ExchangeName, _onlineStoreMqSettings.Value.ExchhangeType);

                    channel.QueueDeclare(queue: _onlineStoreMqSettings.Value.QueueName,
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                    channel.QueueBind(queue: _onlineStoreMqSettings.Value.QueueName, exchange: _onlineStoreMqSettings.Value.ExchangeName, routingKey: _onlineStoreMqSettings.Value.RouteKey);


                    var consumer = new EventingBasicConsumer(channel);

                    BasicGetResult result = channel.BasicGet(_onlineStoreMqSettings.Value.QueueName, true);
                    if (result != null)
                    {
                        string message = Encoding.UTF8.GetString(result.Body);
                        var customer = JsonConvert.DeserializeObject<Customer>(message);
                        _customerRepository.Insert(customer);
                    }

                    channel.BasicConsume(queue: _onlineStoreMqSettings.Value.QueueName, autoAck: false, consumer: consumer);
                }
            }
            catch(Exception)
            {
                
            }
        }
    }
}
