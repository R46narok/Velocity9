using System.ComponentModel.DataAnnotations;

namespace V9.Domain.Entities;

public abstract class EntityBase<T> : IEntity<T>
{
    [Key] 
    public T Id { get; set; } = default!;
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public DateTime UpdatedOn { get; set; } = DateTime.Now;
}