using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityHelper.CommunityService.Models.Dto.Models;
public record CommunityAgentInfo
{
    public Guid Id { get; set; }
    public Guid AgentId { get; set; }
    public Guid CommunityId { get; set; }
}
