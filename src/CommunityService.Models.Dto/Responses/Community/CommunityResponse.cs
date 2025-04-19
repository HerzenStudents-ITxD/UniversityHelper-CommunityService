using UniversityHelper.CommunityService.Models.Dto.Models;

namespace UniversityHelper.CommunityService.Models.Dto.Responses.Community;

public record CommunityResponse
{
    public CommunityInfo Community { get; set; }
    public List<CommunityAgentInfo> Agents { get; set; }
}