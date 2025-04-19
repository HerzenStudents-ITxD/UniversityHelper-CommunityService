using UniversityHelper.CommunityService.Models.Db;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;

namespace UniversityHelper.CommunityService.Data.Interfaces;
[AutoInject]
public interface IHiddenCommunityRepository
{
    Task AddHiddenCommunityAsync(DbCommunityHidden hiddenCommunity);
    Task RemoveHiddenCommunityAsync(Guid userId, Guid communityId);
    Task<bool> IsCommunityHiddenAsync(Guid userId, Guid communityId);
}