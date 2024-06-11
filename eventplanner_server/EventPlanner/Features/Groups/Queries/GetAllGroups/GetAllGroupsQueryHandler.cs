
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Features;
public class GetAllGroupsQueryHandler : IRequestHandler<GetAllGroupsQuery, List<GroupDetailDto>>
{
    private readonly ApplicationDbContext _context;

    public GetAllGroupsQueryHandler(ApplicationDbContext aContext)
    {
        _context = aContext;
    }

    public async Task<List<GroupDetailDto>> Handle(GetAllGroupsQuery aRequest, CancellationToken aCancellationToken)
    {
        var vGroups = await _context.Groups.ToListAsync(aCancellationToken);

        var vGroupDtos = new List<GroupDetailDto>();

        foreach (var vGroupEntity in vGroups)
        {
            vGroupDtos.Add(new GroupDetailDto
            {
                Id = vGroupEntity.Id,
                Name = vGroupEntity.Name
            });
        }

        return vGroupDtos;
    }
}
