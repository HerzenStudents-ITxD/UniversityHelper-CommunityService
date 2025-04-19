using UniversityHelper.CommunityService.Models.Db;
using UniversityHelper.CommunityService.Models.Dto.Requests.News;
using UniversityHelper.Core.Attributes;

namespace UniversityHelper.CommunityService.Mappers.Db.Interfaces;
[AutoInject]
public interface IDbCommunityNewsMapper
{
    DbNews Map(CreateNewsRequest request, Guid userId);
}