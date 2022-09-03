using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using ZeroGravity.Application.Infrastructure.MessageBrokers;
using ZeroGravity.Domain.Events;
using ZeroGravity.Infrastructure.MessageBrokers.RabbitMQ;

namespace ZeroGravity.Infrastructure.MessageBrokers;

public class MessagePublisher : IMessagePublisher
{
    private readonly List<IMessageSenderFactory> _factories;

    public MessagePublisher(IConfiguration configuration, IServiceProvider provider)
    {
        _factories = new();

        var providers = configuration
            .GetSection("Messaging:Providers")
            .Get<string[]>();

        if (providers.Contains("Azure"))
        {
            
        }

        if (providers.Contains("RabbitMQ"))
        {
            var connection = (IConnection)provider.GetService(typeof(IConnection))!; 
            _factories.Add(new RabbitSenderFactory(connection));
        }
    }
    
    public async Task PublishTopicAsync<T>(T message, MessageMetadata metadata, CancellationToken cancellationToken = default) where T : IDomainEvent
    {
        foreach (var factory in _factories)
        {
            var sender = factory.CreateTopicSender<T>();
            await sender.SendAsync(message, metadata, cancellationToken);
        }
    }
    
    public async Task PublishQueueAsync<T>(T message, MessageMetadata metadata, CancellationToken cancellationToken = default) where T : IDomainEvent
    {
        foreach (var factory in _factories)
        {
            var sender = factory.CreateQueueSender<T>();
            await sender.SendAsync(message, metadata, cancellationToken);
        }
    }
}