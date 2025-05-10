using SixLabors.ImageSharp;
using UniversityHelper.CommunityService.Models.Dto.Models;

namespace UniversityHelper.CommunityService.Models.Dto.Responses.News;

public record NewsResponse
{
    public Guid Id { get; set; }
    public Guid CommunityId { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public List<string> Photos { get; set; }
    public List<Guid> Participants { get; set; }
    public Guid? PointId { get; set; }
}