namespace UniversityHelper.CommunityService.Models.Dto.Requests.Community;

public class EditCommunityRequest
{
    public string Name { get; set; }
    public string? AvatarImage { get; set; }
    public string? Text { get; set; }
}