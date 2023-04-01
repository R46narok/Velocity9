using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using V9.Application.Infrastructure.MessageBrokers;
using V9.Infrastructure.MessageBrokers.RabbitMQ;
using V9.Domain.Events;

namespace V9.Infrastructure.MessageBrokers;

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