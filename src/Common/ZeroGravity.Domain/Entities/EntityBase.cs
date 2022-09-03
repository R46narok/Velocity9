namespace ZeroGravity.Domain.Entities;

public abstract class EntityBase<T> : IEntity<T>
{
    public T Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}