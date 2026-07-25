namespace EMSP.Entities.Models;

public class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow.ToLocalTime();
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow.ToLocalTime();
    
    public Guid?  CreatedBy { get; set; }
    public Guid?  UpdatedBy { get; set; }
}