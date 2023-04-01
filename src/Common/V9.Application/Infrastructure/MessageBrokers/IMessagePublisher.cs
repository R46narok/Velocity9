using V9.Domain.Events;

namespace V9.Application.Infrastructure.MessageBrokers;

public interface IMessagePublisher
{
   public Task PublishTopicAsync<T>(T message, MessageMetadata metadata, CancellationToken cancellationToken = default) where T : IDomainEvent;
   public Task PublishQueueAsync<T>(T message, MessageMetadata metadata, CancellationToken cancellationToken = default) where T : IDomainEvent;
}