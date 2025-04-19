using UniversityHelper.CommunityService.Models.Db;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;

namespace UniversityHelper.CommunityService.Data.Interfaces;
[AutoInject]
public interface ICommunityNewsParticipantRepository
{
    Task AddParticipantAsync(DbParticipating participant);
    Task RemoveParticipantAsync(Guid newsId, Guid userId);
    Task<bool> IsParticipantAsync(Guid newsId, Guid userId);
}