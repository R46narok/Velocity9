using ZeroGravity.Domain.Events;

namespace ZeroGravity.Application.Infrastructure.MessageBrokers;

public interface IMessageSender<in T> where T : IDomainEvent
{
   public Task SendAsync(T message, MessageMetadata metadata, CancellationToken cancellationToken = default);
}
