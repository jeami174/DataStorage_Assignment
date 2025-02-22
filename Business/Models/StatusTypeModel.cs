namespace Business.Models;

public class StatusTypeModel
{
    public int Id { get; set; }
    public string StatusTypeName { get; set; } = null!;
    public ICollection<ProjectModel> Projects { get; set; } = null!;

}
