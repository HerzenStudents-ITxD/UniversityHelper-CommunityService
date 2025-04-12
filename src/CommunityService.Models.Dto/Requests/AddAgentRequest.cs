using System;
using System.ComponentModel.DataAnnotations;

namespace UniversityHelper.CommunityService.Models.Dto.Requests.Community;

public class AddAgentRequest
{
    [Required]
    public Guid CommunityId { get; set; }
    [Required]
    public Guid AgentId { get; set; }
}
