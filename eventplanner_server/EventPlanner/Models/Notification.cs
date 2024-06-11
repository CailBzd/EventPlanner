namespace EventPlanner.Models;
public class Notification
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public DateTime Date { get; set; }
}