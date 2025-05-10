using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityHelper.CommunityService.Models.Dto.Models;

namespace UniversityHelper.CommunityService.Models.Dto.Responses.Community;
public record CommunitiesResponse
{
    public List<CommunityAdminInfo> Communities { get; set; }
}
