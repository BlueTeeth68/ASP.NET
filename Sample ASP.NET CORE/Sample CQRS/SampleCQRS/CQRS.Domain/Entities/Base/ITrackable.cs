namespace CQRS.Domain.Entities.Base;

public interface ITrackable
{
    public byte[] RowVersion { get; set; }
    
    public DateTimeOffset CreatedDateTime { get; set; }
    
    public DateTimeOffset? UpdatedDateTime { get; set; }
}