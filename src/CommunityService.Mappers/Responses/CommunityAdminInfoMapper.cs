using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityHelper.CommunityService.Models.Db;
using UniversityHelper.CommunityService.Models.Dto.Models;

namespace UniversityHelper.CommunityService.Mappers.Responses;
public class CommunityAdminInfoMapper
{
    public CommunityAdminInfo Map(DbCommunity community)
    {
        return new CommunityAdminInfo
        {
            Id = community.Id,
            Name = community.Name,
            Avatar = community.Avatar,
            Text = community.Text ?? "",
            AuthorId = community.CreatedBy, // Assuming AuthorId is CreatedBy
            PointId = null, // Adjust if applicable
            Agents = community.Agents.Select(a => new CommunityAgentInfo { UserId = a.AgentId }).ToList(),
            CreatedBy = community.CreatedBy,
            CreatedAtUtc = community.CreatedAtUtc,
            ModifiedBy = community.ModifiedBy,
            ModifiedAtUtc = community.ModifiedAtUtc
        };
    }
}
