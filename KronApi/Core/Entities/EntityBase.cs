namespace KronApi.Core.Entities;

public abstract class EntityBase
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
}