using UniversityHelper.CommunityService.Models.Db;
using UniversityHelper.CommunityService.Models.Dto.Requests.Community;
using UniversityHelper.Core.Attributes;

namespace UniversityHelper.CommunityService.Mappers.Db.Interfaces;
[AutoInject]
public interface IDbCommunityMapper
{
    DbCommunity Map(CreateCommunityRequest request);
}