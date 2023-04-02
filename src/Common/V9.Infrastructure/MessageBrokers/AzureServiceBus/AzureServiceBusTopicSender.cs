using System.Text;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using V9.Application.Infrastructure.MessageBrokers;
using V9.Domain.Events;

namespace V9.Infrastructure.MessageBrokers.AzureServiceBus;

public class AzureServiceBusTopicSender<T> : IMessageSender<T> where T : IDomainEvent
{
    private readonly ServiceBusSender _sender;

    public AzureServiceBusTopicSender(ServiceBusSender sender)
    {
        _sender = sender ?? throw new ArgumentNullException(nameof(sender));
    }

    public async Task SendAsync(T message, MessageMetadata metadata, CancellationToken cancellationToken = default)
    {
        if (message is null)
            throw new ArgumentNullException(nameof(message));

        if (metadata is null)
            throw new ArgumentNullException(nameof(metadata));

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        var messageData = new ServiceBusMessage(body)
        {
            ContentType = "application/json",
        };
        
        await _sender.SendMessageAsync(messageData, cancellationToken).ConfigureAwait(false);
    }
}
