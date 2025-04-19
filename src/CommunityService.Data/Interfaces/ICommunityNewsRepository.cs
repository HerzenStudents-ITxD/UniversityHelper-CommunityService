using UniversityHelper.CommunityService.Models.Db;
using System.Threading;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;

namespace UniversityHelper.CommunityService.Data.Interfaces;
[AutoInject]
public interface ICommunityNewsRepository
{
    Task CreateAsync(DbNews news);
    Task<(List<DbNews> news, int totalCount)> FindAsync(
      Guid userId, int page, int pageSize, CancellationToken cancellationToken);
    Task<DbNews> GetAsync(Guid newsId, CancellationToken cancellationToken);
}