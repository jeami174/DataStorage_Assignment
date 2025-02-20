namespace Business.Dtos;

public class ServiceCreateDto
{
    public string ServiceName { get; set; } = null!;
    public decimal PricePerUnit { get; set; }

    public int UnitId { get; set; }
}
