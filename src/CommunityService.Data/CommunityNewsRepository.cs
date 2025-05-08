using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityHelper.CommunityService.Data.Interfaces;
using UniversityHelper.CommunityService.Data.Provider;
using UniversityHelper.CommunityService.Models.Db;

namespace UniversityHelper.CommunityService.Data;

public class CommunityNewsRepository : ICommunityNewsRepository
{
    private readonly IDataProvider _provider;

    public CommunityNewsRepository(IDataProvider provider)
    {
        _provider = provider;
    }

    public async Task CreateAsync(DbNews news)
    {
        await _provider.News.AddAsync(news);
        await _provider.SaveAsync();
    }

    public async Task<(List<DbNews> news, int totalCount)> FindAsync(
        int page, int pageSize, CancellationToken cancellationToken)
    {
        var query = _provider.News
            .Include(n => n.Photos)
            .Include(n => n.Participatings)
            .AsQueryable();

        var totalCount = await query.CountAsync(cancellationToken);
        var news = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (news, totalCount);
    }

    public async Task<DbNews> GetAsync(Guid newsId, CancellationToken cancellationToken)
    {
        return await _provider.News
            .Include(n => n.Photos)
            .Include(n => n.Participatings)
            .FirstOrDefaultAsync(n => n.Id == newsId, cancellationToken);
    }
}