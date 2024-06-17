using EventPlanner.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EventPlanner.Features;
public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
{
        private readonly UserManager<User> _userManager;

    public GetUserByIdQueryHandler(UserManager<User> aUserManager)
    {
            _userManager = aUserManager;
    }

    public async Task<User> Handle(GetUserByIdQuery aRequest, CancellationToken aCancellationToken)
    {
            var vUser = await _userManager.FindByIdAsync(aRequest.Id.ToString());
            return vUser;
    }

}
