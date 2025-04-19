using System.ComponentModel.DataAnnotations;

namespace UniversityHelper.CommunityService.Models.Dto.Requests.Community;

public record AddAgentRequest
{
    [Required]
    public Guid CommunityId { get; set; }
    [Required]
    public Guid UserId { get; set; }
    public bool IsModerator { get; set; }
}