namespace Business.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int QuantityofServiceUnits { get; set; }
        public decimal TotalPrice { get; set; }

        public CustomerModel Customer { get; set; } = null!;
        public ServiceModel Service { get; set; } = null!;
        public StatusTypeModel Status { get; set; } = null!;
        public UserModel User { get; set; } = null!;
    }
}

