using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using V9.Application.Infrastructure.MessageBrokers;
using V9.Domain.Events;

namespace V9.Infrastructure.MessageBrokers.RabbitMQ;

public class RabbitTopicSender<T> : IMessageSender<T> where T : IDomainEvent
{
    private readonly IConnection _connection;
    
    public RabbitTopicSender(IConnection connection)
    {
        _connection = connection;
    }
    
    public Task SendAsync(T message, MessageMetadata metadata, CancellationToken cancellationToken = default)
    {
        var channel = _connection.CreateModel();
        var name = typeof(T).Name;
        
        channel.ExchangeDeclare(name, ExchangeType.Fanout);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);
        
        channel.BasicPublish(name, routingKey: "", basicProperties: null, body);
        
        return Task.CompletedTask;
    }
}