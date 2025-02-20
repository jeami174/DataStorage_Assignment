
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class ProjectEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string Title { get; set; } = null!;

    [Column(TypeName = "nvarchar(max)")]
    public string? Description { get; set; }

    [Column(TypeName = "Date")]
    public DateTime StartDate { get; set; }

    [Column(TypeName = "Date")]
    public DateTime EndDate { get; set; }

    [Required]
    public decimal TotalPrice { get; set; }




    [ForeignKey("Customer")]
    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;

    [ForeignKey("Service")]
    public int ServiceId { get; set; }
    public ServiceEntity Service { get; set; } = null!;

    [ForeignKey("StatusType")]
    public int StatusId { get; set; }
    public StatusTypeEntity Status { get; set; } = null!;

    [ForeignKey("User")]
    public int UserId { get; set; }
    public UserEntity User { get; set; } = null!;

}
