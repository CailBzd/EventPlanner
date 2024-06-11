using MediatR;

namespace EventPlanner.Features;
    public class GetAllUsersQuery : IRequest<List<UserDetailDto>>
    {
    }
