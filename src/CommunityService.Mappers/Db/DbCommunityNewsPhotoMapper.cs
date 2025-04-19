using UniversityHelper.CommunityService.Mappers.Db.Interfaces;
using UniversityHelper.CommunityService.Models.Db;

namespace UniversityHelper.CommunityService.Mappers.Db;

public class DbCommunityNewsPhotoMapper : IDbCommunityNewsPhotoMapper
{
    public DbNewsPhoto Map(string photo, Guid newsId)
    {
        return new DbNewsPhoto
        {
            Id = Guid.NewGuid(),
            NewsId = newsId,
            Photo = photo
        };
    }
}