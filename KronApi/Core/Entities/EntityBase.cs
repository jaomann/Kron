namespace KronApi.Core.Entities;

public abstract class EntityBase
{
    public Guid id { get; set; }
    public bool isDeleted { get; set; }
}