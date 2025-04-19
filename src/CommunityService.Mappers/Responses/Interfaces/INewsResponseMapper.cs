using SixLabors.ImageSharp;
using UniversityHelper.CommunityService.Models.Db;
using UniversityHelper.CommunityService.Models.Dto.Models;
using UniversityHelper.CommunityService.Models.Dto.Responses.News;
using UniversityHelper.Core.Attributes;

namespace UniversityHelper.CommunityService.Mappers.Responses;
[AutoInject]
public interface INewsResponseMapper
{
    NewsResponse Map(DbNews news, List<string> images);
}