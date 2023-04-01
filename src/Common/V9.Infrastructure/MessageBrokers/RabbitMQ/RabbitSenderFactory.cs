using RabbitMQ.Client;
using V9.Application.Infrastructure.MessageBrokers;
using V9.Domain.Events;

namespace V9.Infrastructure.MessageBrokers.RabbitMQ;

public class RabbitSenderFactory : IMessageSenderFactory
{
    private readonly IConnection _connection;

    public RabbitSenderFactory(IConnection connection)
    {
        _connection = connection;
    }
    
    public IMessageSender<T> CreateTopicSender<T>() where T : IDomainEvent
    {
        return new RabbitTopicSender<T>(_connection);
    }

    public IMessageSender<T> CreateQueueSender<T>() where T : IDomainEvent
    {
        return new RabbitQueueSender<T>(_connection);
    }
}