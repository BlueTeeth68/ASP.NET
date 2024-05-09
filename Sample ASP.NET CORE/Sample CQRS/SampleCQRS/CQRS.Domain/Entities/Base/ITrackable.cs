namespace CQRS.Domain.Entities.Base;

public interface ITrackable
{
    public byte[] RowVersion { get; set; }
    
    public DateTime CreatedDateTime { get; set; }
    
    public DateTime? UpdatedDateTime { get; set; }
}