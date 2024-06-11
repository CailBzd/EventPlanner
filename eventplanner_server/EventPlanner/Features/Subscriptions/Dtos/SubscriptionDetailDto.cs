namespace EventPlanner.Features;
public class SubscriptionDetailDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Plan { get; set; }
    public decimal Price { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
