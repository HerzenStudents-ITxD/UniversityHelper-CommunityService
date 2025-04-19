using System.ComponentModel.DataAnnotations;

namespace UniversityHelper.CommunityService.Models.Dto.Requests.Community;

public record ParticipateRequest
{
    [Required]
    public Guid NewsId { get; set; }
}