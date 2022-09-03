using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using ZeroGravity.Application.Infrastructure.MessageBrokers;
using ZeroGravity.Domain.Events;

namespace ZeroGravity.Infrastructure.MessageBrokers.RabbitMQ;

public class RabbitQueueSender<T> : IMessageSender<T> where T : IDomainEvent
{
    private readonly IConnection _connection;
    
    public RabbitQueueSender(IConnection connection)
    {
        _connection = connection;
    }
    
    public Task SendAsync(T message, MessageMetadata metadata, CancellationToken cancellationToken = default)
    {
        var channel = _connection.CreateModel();
        var name = typeof(T).Name;
        
        channel.QueueDeclare(name, false, false, false, null);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);
        
        channel.BasicPublish("", routingKey: name, basicProperties: null, body);
        
        return Task.CompletedTask;
    }
}