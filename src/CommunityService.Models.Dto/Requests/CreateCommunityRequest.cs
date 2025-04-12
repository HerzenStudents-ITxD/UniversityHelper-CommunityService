using System;
using System.ComponentModel.DataAnnotations;

namespace UniversityHelper.CommunityService.Models.Dto.Requests.Community;

public class CreateCommunityRequest
{
    [Required]
    public string Name { get; set; }
    public string Avatar { get; set; }


}
