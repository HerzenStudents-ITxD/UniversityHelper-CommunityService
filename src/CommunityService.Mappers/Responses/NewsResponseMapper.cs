using SixLabors.ImageSharp;
using UniversityHelper.CommunityService.Models.Db;
using UniversityHelper.CommunityService.Models.Dto.Models;
using UniversityHelper.CommunityService.Models.Dto.Responses.News;

namespace UniversityHelper.CommunityService.Mappers.Responses;

public class NewsResponseMapper : INewsResponseMapper
{
    public NewsResponse Map(DbNews news, List<string> photos)
    {
        return new NewsResponse
        {
            Id = news.Id,
            CommunityId = news.CommunityId,
            Title = news.Title,
            Text = news.Text,
            Photos = photos,
            Participants = news.Participatings?.Select(p => p.UserId).ToList() ?? new List<Guid>()
        };
    }
}