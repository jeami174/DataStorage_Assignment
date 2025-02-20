namespace Business.Models;

public class ServiceModel
{
    public int Id { get; set; }
    public string ServiceName { get; set; } = null!;
    public decimal PricePerUnit { get; set; }
    public UnitModel Unit { get; set; } = null!;

    public override string ToString()
    {
        return $"{ServiceName} ({PricePerUnit} {Unit.UnitName})";
    } 
}
