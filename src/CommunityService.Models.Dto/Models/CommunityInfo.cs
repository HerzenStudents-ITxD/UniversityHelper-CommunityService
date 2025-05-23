﻿using SixLabors.ImageSharp;

namespace UniversityHelper.CommunityService.Models.Dto.Models;

public record CommunityInfo
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsHidden { get; set; }
    public string? Avatar { get; set; }
    public string? Text { get; set; }
    public Guid CreatedBy { get; set; }        
    public DateTime CreatedAtUtc { get; set; } 
    public Guid ModifiedBy { get; set; }       
    public DateTime ModifiedAtUtc { get; set; } 
}