using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityHelper.CommunityService.Models.Db;
using UniversityHelper.Core.Attributes;

namespace UniversityHelper.CommunityService.Data.Interfaces;
[AutoInject]
public interface ICommunityRepository
{
    Task <List<(DbCommunity community, List<DbCommunityAgent>)>>GetAllWithAgentsAsync();
    Task CreateAsync(DbCommunity community);
    Task<bool> AddAgentAsync(Guid communityId, Guid agentId);
}
