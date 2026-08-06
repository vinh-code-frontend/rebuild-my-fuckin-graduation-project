namespace App.Domain.Entities;

public abstract class TimestampWithSoftDeleteEntity : TimestampEntity
{
    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }
}
