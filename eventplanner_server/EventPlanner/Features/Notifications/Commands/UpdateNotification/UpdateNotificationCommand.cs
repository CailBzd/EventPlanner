using MediatR;

namespace EventPlanner.Features;
    public class UpdateNotificationCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
