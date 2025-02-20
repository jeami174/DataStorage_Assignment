namespace Business.Models;

public class CustomerModel
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = null!;
    public CustomerTypeModel CustomerType { get; set; } = null!;
    public ICollection<CustomerContactModel> CustomerContacts { get; set; } = null!;

    public override string ToString()
    {
        return $"{CustomerName} ({CustomerType.CustomerTypeName})";
    }
}
