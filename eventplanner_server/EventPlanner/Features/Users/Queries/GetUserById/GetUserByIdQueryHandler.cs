using MediatR;

namespace EventPlanner.Features;
public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDetailDto>
{
    private readonly ApplicationDbContext _context;

    public GetUserByIdQueryHandler(ApplicationDbContext aContext)
    {
        _context = aContext;
    }

    public async Task<UserDetailDto> Handle(GetUserByIdQuery aRequest, CancellationToken aCancellationToken)
    {
        var vUserEntity = await _context.Users.FindAsync(aRequest.Id);

        if (vUserEntity == null)
        {
            return null;
        }

        return new UserDetailDto
        {
            Id = vUserEntity.Id,
            UserName = vUserEntity.UserName,
            Email = vUserEntity.Email
        };
    }

}
