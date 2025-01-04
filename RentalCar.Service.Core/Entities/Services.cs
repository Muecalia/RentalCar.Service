namespace RentalCar.Service.Core.Entities;

public class Services
{
    public Services()
    {
        IsDeleted = false;
        Id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
}