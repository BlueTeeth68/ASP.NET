using System.ComponentModel.DataAnnotations;

namespace CQRS.Domain.Entities.Base;

public abstract class BaseEntity<TKey> : IHasKey<TKey>, ITrackable
{
    [Key] public TKey Id { get; set; } = default(TKey);

    [Timestamp] public byte[] RowVersion { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public DateTime? UpdatedDateTime { get; set; }
}