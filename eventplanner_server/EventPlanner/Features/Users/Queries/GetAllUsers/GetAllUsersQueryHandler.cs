using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Features;
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDetailDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllUsersQueryHandler(ApplicationDbContext aContext)
        {
            _context = aContext;
        }

        public async Task<List<UserDetailDto>> Handle(GetAllUsersQuery aRequest, CancellationToken aCancellationToken)
        {
            var vUsers = await _context.Users.ToListAsync(aCancellationToken);

            var vUserDtos = new List<UserDetailDto>();

            foreach (var vUserEntity in vUsers)
            {
                vUserDtos.Add(new UserDetailDto
                {
                    Id = vUserEntity.Id,
                    UserName = vUserEntity.UserName,
                    Email = vUserEntity.Email
                });
            }

            return vUserDtos;
        }
    }
