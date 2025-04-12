using System.Collections.Generic;
using System.Linq;
using UniversityHelper.CommunityService.Models.Db;
using UniversityHelper.CommunityService.Models.Dto.Models;
//using UniversityHelper.CommunityService.Models.Dto.Responses.Community;

namespace UniversityHelper.CommunityService.Mappers.Db;

public static class DbCommunityMapper
{
    public static CommunityAdminInfo Map(DbCommunity dbCommunity, List<DbCommunityAgent> dbAgents)
    {
        return new CommunityAdminInfo
        {
            Id = dbCommunity.Id,
            Name = dbCommunity.Name,
            Avatar = dbCommunity.Avatar,
            Agents = dbAgents.Select(a => new CommunityAgentInfo { Id = a.Id, AgentId = a.AgentId, CommunityId = a.CommunityId }).ToList()
        };
    }
}
