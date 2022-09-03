using ZeroGravity.Domain.Events;

namespace ZeroGravity.Application.Infrastructure.MessageBrokers;

public interface IMessageSenderFactory
{
    public IMessageSender<T> CreateTopicSender<T>() where T : IDomainEvent;
    public IMessageSender<T> CreateQueueSender<T>() where T : IDomainEvent;
}