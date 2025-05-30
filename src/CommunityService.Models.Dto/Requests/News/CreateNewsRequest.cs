﻿using System.ComponentModel.DataAnnotations;

namespace UniversityHelper.CommunityService.Models.Dto.Requests.News;

public record CreateNewsRequest
{
    [Required]
    public Guid CommunityId { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Text { get; set; }
    public List<string>? Images { get; set; }
    public Guid? PointId { get; set; }
}