using RabbitMQ.Client;
using ZeroGravity.Application.Infrastructure.MessageBrokers;
using ZeroGravity.Domain.Events;

namespace ZeroGravity.Infrastructure.MessageBrokers.RabbitMQ;

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