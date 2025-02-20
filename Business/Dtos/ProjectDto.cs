using System;

namespace Business.Dtos;
public class ProjectDto
{

    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string StatusName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public bool IsDetailsVisible { get; set; }


    public int CustomerId { get; set; }
    public int StatusId { get; set; }
    public int UserId { get; set; }


}
