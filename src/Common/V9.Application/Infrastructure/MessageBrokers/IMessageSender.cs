using V9.Domain.Events;

namespace V9.Application.Infrastructure.MessageBrokers;

public interface IMessageSender<in T> where T : IDomainEvent
{
   public Task SendAsync(T message, MessageMetadata metadata, CancellationToken cancellationToken = default);
}
