namespace App.Domain.Entities;

public abstract class TimestampEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
