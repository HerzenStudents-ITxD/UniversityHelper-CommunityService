using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityHelper.CommunityService.Models.Dto.Models;
public record CommunityAdminInfo
{    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Avatar { get; set; }
    public string Text { get; set; }

    public Guid AuthorId { get; set; }
    public Guid? PointId { get; set; }
    public List<CommunityAgentInfo> Agents { get; set; }
}
