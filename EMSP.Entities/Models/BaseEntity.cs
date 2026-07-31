using System.ComponentModel.DataAnnotations;

namespace EMSP.Entities.Models;

public class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
    
    public Guid?  CreatedBy { get; set; }
    public Guid?  UpdatedBy { get; set; }
}