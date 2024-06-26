namespace EventPlanner.Features;
public class EventDetailDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; }    
    public byte[]? Image { get; set; }
}
