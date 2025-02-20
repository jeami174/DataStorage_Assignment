namespace Business.Models;

public class CustomerContactModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int CustomerId { get; set; }
    public CustomerModel Customer { get; set; } = null!;
}
