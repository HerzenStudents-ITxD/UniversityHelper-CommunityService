namespace UniversityHelper.CommunityService.Models.Dto.Requests.Community;

public class EditCommunityRequest
{
    public string Name { get; set; }
    public string? AvatarImage { get; set; } // Сделали nullable
    public string? Text { get; set; } // Добавили новое поле
}