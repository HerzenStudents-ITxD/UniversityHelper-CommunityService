using Microsoft.AspNetCore.JsonPatch;
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

public class CommunityRepository : ICommunityRepository
{
    private readonly IDataProvider _provider;

    public CommunityRepository(IDataProvider provider)
    {
        _provider = provider;
    }

    public async Task CreateAsync(DbCommunity community)
    {
        await _provider.Communities.AddAsync(community);
        await _provider.SaveAsync();
    }

    public async Task<(List<DbCommunity> communities, int totalCount)> FindAsync(
        Guid? userId,
        bool includeAgents,
        bool includeAvatars,
        CancellationToken cancellationToken)
    {
        var query = _provider.Communities.AsQueryable();
        if (userId.HasValue)
        {
            query = query.Where(c => c.Agents.Any(a => a.AgentId == userId.Value));
        }
        if (includeAgents)
        {
            query = query.Include(c => c.Agents);
        }
        var totalCount = await query.CountAsync(cancellationToken);
        var communities = await query.ToListAsync(cancellationToken);
        return (communities, totalCount);
    }

    public async Task<DbCommunity> GetAsync(Guid communityId, CancellationToken cancellationToken)
    {
        return await _provider.Communities
            .Include(c => c.Agents)
            .FirstOrDefaultAsync(c => c.Id == communityId, cancellationToken);
    }

    public async Task<bool> UpdateAsync(DbCommunity community)
    {
        _provider.Communities.Update(community);
        await _provider.SaveAsync(); 
        return true;
    }

    public async Task<bool> SoftDeleteAsync(Guid communityId)
    {
        var community = await GetAsync(communityId, CancellationToken.None);
        if (community == null)
            return false;
        _provider.Communities.Remove(community);
        await _provider.SaveAsync();
        return true;

    }
    public async Task<bool> UpdateAsync(Guid communityId, Guid userId, JsonPatchDocument<DbCommunity> patch)
    {
        // Получаем сообщество из базы данных
        DbCommunity dbCommunity = await _provider.Communities
            .FirstOrDefaultAsync(x => x.Id == communityId);

        // Проверяем валидность входных данных
        if (patch is null || dbCommunity is null)
        {
            return false;
        }

        try
        {
            // Применяем изменения из patch
            patch.ApplyTo(dbCommunity);

            // Обновляем метаданные
            dbCommunity.ModifiedBy = userId;
            dbCommunity.ModifiedAtUtc = DateTime.UtcNow;

            // Сохраняем изменения
            await _provider.SaveAsync();

            return true;
        }
        catch (Exception ex)
        {
            // Логирование ошибки при необходимости
            //_logger.LogError(ex, "Error editing community with ID {CommunityId}", communityId);
            return false;
        }
    }

    public Task<bool> UpdateAsync(Guid communityId, JsonPatchDocument<DbCommunity> dbPatch)
    {
        throw new NotImplementedException();
    }
}