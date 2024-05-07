using System.ComponentModel.DataAnnotations;

namespace CQRS.Domain.Entities.Base;

public class BaseEntity<TKey> : IHasKey<TKey>, ITrackable
{
    public TKey Id { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    public DateTimeOffset CreatedDateTime { get; set; }

    public DateTimeOffset? UpdatedDateTime { get; set; }
}