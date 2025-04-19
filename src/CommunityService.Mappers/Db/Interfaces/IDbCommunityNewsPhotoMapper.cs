using UniversityHelper.CommunityService.Models.Db;
using UniversityHelper.Core.Attributes;

namespace UniversityHelper.CommunityService.Mappers.Db.Interfaces;
[AutoInject]
public interface IDbCommunityNewsPhotoMapper
{
    DbNewsPhoto Map(string photo, Guid newsId);
}