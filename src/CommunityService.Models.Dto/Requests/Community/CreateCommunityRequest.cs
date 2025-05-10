using System.ComponentModel.DataAnnotations;

namespace UniversityHelper.CommunityService.Models.Dto.Requests.Community;

public record CreateCommunityRequest
{
    [Required]
    public string Name { get; set; }
    public string? AvatarImage { get; set; } // Сделали nullable
    public string? Text { get; set; } // Добавили новое поле
}