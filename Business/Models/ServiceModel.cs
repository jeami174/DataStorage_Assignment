namespace Business.Models;

public class ServiceModel
{
    public int Id { get; set; }
    public string ServiceName { get; set; } = null!;
    public decimal PricePerUnit { get; set; }
    public int UnitId { get; set; }
    public UnitModel Unit { get; set; } = null!;

    public ICollection<ProjectModel> Projects { get; set; } = null!;


}
