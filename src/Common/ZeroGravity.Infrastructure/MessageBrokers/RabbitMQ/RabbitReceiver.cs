using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ZeroGravity.Application.Infrastructure.MessageBrokers;
using ZeroGravity.Domain.Events;

namespace ZeroGravity.Infrastructure.MessageBrokers.RabbitMQ;

public class RabbitReceiver<T> : IMessageReceiver where T : IDomainEvent
{
    private readonly IServiceProvider _provider;
    private readonly IModel _channel;
    private readonly string _queueName;
    private readonly EventingBasicConsumer _consumer;
    
    public RabbitReceiver(IConnection connection, IServiceProvider provider, string exchange)
    {
        _provider = provider;
        _channel = connection.CreateModel();
        _queueName = _channel.QueueDeclare().QueueName;
        
        _channel.ExchangeDeclare(exchange, ExchangeType.Fanout);
        
        _channel.QueueBind(_queueName, exchange, routingKey: "");

        _consumer = new EventingBasicConsumer(_channel);
        _consumer.Received += ConsumerOnReceived;

        _channel.BasicConsume(_queueName, autoAck: true, _consumer);
    }

    private void ConsumerOnReceived(object? sender, BasicDeliverEventArgs e)
    {
        var json = Encoding.UTF8.GetString(e.Body.Span);
        var @event = JsonSerializer.Deserialize<T>(json);

        using var scope = _provider.CreateScope();
        var handler = scope.ServiceProvider.GetService<IDomainEventHandler<T>>();
        handler!.HandleAsync(@event).Wait();
    }
}