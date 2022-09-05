using System.ComponentModel.DataAnnotations;

namespace ZeroGravity.Domain.Entities;

public interface IEntity<T>
{
    [Key]
    public T Id { get; set; } 
    public DateTime CreatedOn { get; set; } 
    public DateTime UpdatedOn { get; set; } 
}