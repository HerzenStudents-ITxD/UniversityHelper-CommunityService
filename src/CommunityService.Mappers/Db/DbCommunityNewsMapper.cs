using UniversityHelper.CommunityService.Mappers.Db.Interfaces;
using UniversityHelper.CommunityService.Models.Db;
using UniversityHelper.CommunityService.Models.Dto.Requests.News;

namespace UniversityHelper.CommunityService.Mappers.Db;

public class DbCommunityNewsMapper : IDbCommunityNewsMapper
{
    public DbNews Map(CreateNewsRequest request, Guid userId)
    {
        return new DbNews
        {
            Id = Guid.NewGuid(),
            CommunityId = request.CommunityId,
            Title = request.Title,
            Text = request.Text,
            AuthorId = userId,
            Date = DateTime.UtcNow,
            PointId = request.PointId
        };
    }
}