using MediatR;
using System;

namespace EventPlanner.Features;
public class CreateEventCommand : IRequest<Guid>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; }
}
