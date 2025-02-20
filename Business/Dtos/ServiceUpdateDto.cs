
namespace Business.Dtos;

public class ServiceUpdateDto
{
    public string ServiceName { get; set; } = null!;
    public decimal PricePerUnit { get; set; }

    public int UnitId { get; set; }
}
