using SixLabors.ImageSharp;

namespace UniversityHelper.CommunityService.Models.Dto.Models;

public record CommunityInfo
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsHidden { get; set; }
    public string? Avatar { get; set; } // Nullable
    public string? Text { get; set; } // Nullable
}