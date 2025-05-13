using UniversityHelper.CommunityService.Models.Db;
using System.Threading;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;

namespace UniversityHelper.CommunityService.Data.Interfaces;
[AutoInject]
public interface ICommunityNewsRepository
{
    Task AddAsync(DbNews news);
    Task<(List<DbNews> news, int totalCount)> FindAsync(int page, int pageSize, CancellationToken cancellationToken);
    Task<DbNews> GetAsync(Guid newsId, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(DbNews news); 
    Task<bool> DeleteAsync(Guid newsId); 
}