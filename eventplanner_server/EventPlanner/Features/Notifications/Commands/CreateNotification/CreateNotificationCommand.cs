using MediatR;

namespace EventPlanner.Features;
    public class CreateNotificationCommand : IRequest<Guid>
    {
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
