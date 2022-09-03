using ZeroGravity.Domain.Events;

namespace ZeroGravity.Application.Infrastructure.MessageBrokers;

public class Message<T> where T : IDomainEvent
{
   public T Data { get; set; }
   public MessageMetadata Metadata { get; set; }
}