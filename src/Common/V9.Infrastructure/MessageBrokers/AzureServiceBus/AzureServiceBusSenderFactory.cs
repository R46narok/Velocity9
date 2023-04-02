using Azure.Messaging.ServiceBus;
using RabbitMQ.Client;
using V9.Application.Infrastructure.MessageBrokers;
using V9.Domain.Events;
using V9.Infrastructure.MessageBrokers.RabbitMQ;

namespace V9.Infrastructure.MessageBrokers.AzureServiceBus;

public class AzureServiceBusSenderFactory : IMessageSenderFactory
{
    private readonly ServiceBusClient _client;

    public AzureServiceBusSenderFactory(string connectionString)
    {
        _client = new ServiceBusClient(connectionString);
    }

    public IMessageSender<T> CreateTopicSender<T>() where T : IDomainEvent
    {
        var sender = _client.CreateSender(typeof(T).Name);
        return new AzureServiceBusTopicSender<T>(sender);
    }

    public IMessageSender<T> CreateQueueSender<T>() where T : IDomainEvent
    {
        throw new NotImplementedException();
    }
}

