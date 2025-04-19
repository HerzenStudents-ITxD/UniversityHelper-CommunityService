using UniversityHelper.CommunityService.Mappers.Db.Interfaces;
using UniversityHelper.CommunityService.Models.Db;
using UniversityHelper.CommunityService.Models.Dto.Requests.Community;
using Microsoft.AspNetCore.Http;

namespace UniversityHelper.CommunityService.Mappers.Db;

public class DbCommunityMapper : IDbCommunityMapper
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DbCommunityMapper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public DbCommunity Map(CreateCommunityRequest request)
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value ?? Guid.Empty.ToString();
        return new DbCommunity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Avatar = request.AvatarImage ?? string.Empty,
            CreatedBy = Guid.Parse(userId),
            CreatedAtUtc = DateTime.UtcNow
        };
    }
}