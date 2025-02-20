
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace Data.Entities;

[Index(nameof(CustomerName), IsUnique = true)]
public class CustomerEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string CustomerName { get; set; } = null!;


    [ForeignKey("CustomerType")]
    public int CustomerTypeId { get; set; }
    public CustomerTypeEntity CustomerType { get; set; } = null!;

    public ICollection<ProjectEntity> Projects { get; set; } = [];
    public ICollection<CustomerContactEntity> Contacts { get; set; } = [];


}
