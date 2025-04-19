using UniversityHelper.CommunityService.Models.Db;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;

namespace UniversityHelper.CommunityService.Data.Interfaces;
[AutoInject]
public interface ICommunityAgentRepository
{
    Task CreateAsync(DbCommunityAgent agent);
    Task<bool> IsAgentAsync(Guid userId, Guid communityId);
    Task<bool> IsModeratorAsync(Guid userId, Guid communityId);
    Task RemoveAgentAsync(Guid communityId, Guid userId);

}