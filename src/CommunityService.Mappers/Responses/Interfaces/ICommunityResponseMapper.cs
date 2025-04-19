using SixLabors.ImageSharp;
using UniversityHelper.CommunityService.Models.Db;
using UniversityHelper.CommunityService.Models.Dto.Models;
using UniversityHelper.CommunityService.Models.Dto.Responses.Community;
using UniversityHelper.Core.Attributes;

namespace UniversityHelper.CommunityService.Mappers.Responses.Interfaces;
[AutoInject]
public interface ICommunityResponseMapper
{
    CommunityResponse Map(DbCommunity community, List<string> images);
}