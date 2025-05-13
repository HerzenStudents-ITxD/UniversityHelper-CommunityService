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

    public async Task AddAsync(DbNews news)
    {
        await _provider.News.AddAsync(news); // Добавляем, но не сохраняем
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

    public async Task<bool> UpdateAsync(DbNews news)
    {
        _provider.News.Update(news);
        await _provider.SaveAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid newsId)
    {
        var news = await GetAsync(newsId, CancellationToken.None);
        if (news == null)
            return false;
        _provider.News.Remove(news);
        await _provider.SaveAsync();
        return true;
    }
}