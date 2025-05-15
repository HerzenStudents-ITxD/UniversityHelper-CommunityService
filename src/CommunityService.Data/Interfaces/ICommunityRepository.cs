using UniversityHelper.CommunityService.Models.Db;
using System.Threading;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;
using Microsoft.AspNetCore.JsonPatch;

namespace UniversityHelper.CommunityService.Data.Interfaces;
[AutoInject]
public interface ICommunityRepository
{
    Task CreateAsync(DbCommunity community);
    Task<(List<DbCommunity> communities, int totalCount)> FindAsync(
      Guid? userId,
      bool includeAgents,
      bool includeAvatars,
      CancellationToken cancellationToken);
    Task<DbCommunity> GetAsync(Guid communityId, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(DbCommunity community);
    Task<bool> SoftDeleteAsync(Guid communityId);
    Task<bool> UpdateAsync(Guid communityId, Guid userId, JsonPatchDocument<DbCommunity> patch);
    Task<bool> UpdateAsync(Guid communityId, JsonPatchDocument<DbCommunity> dbPatch);
}