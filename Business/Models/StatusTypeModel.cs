namespace Business.Models;

public class StatusTypeModel
{
    public int Id { get; set; }
    public string StatusTypeName { get; set; } = null!;

    public override string ToString()
    {
        return StatusTypeName;
    }
}
