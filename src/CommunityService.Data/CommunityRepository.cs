using MassTransit.Testing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

    public async Task<DbCommunity> GetByIdAsync(Guid communityId)
    {
        return await _provider.Communities
            .Include(c => c.Agents)
            .Include(c => c.News)
            .ThenInclude(n => n.Photos)
            .FirstOrDefaultAsync(c => c.Id == communityId);
    }


    public async Task<bool> AddAgentAsync(Guid communityId, Guid agentId)
    {
        var community = await GetByIdAsync(communityId);
        if (community == null)
            return false;

        community.Agents.Add(new DbCommunityAgent { AgentId = agentId });
        await _provider.SaveAsync();
        return true;
    }

    public async Task<List<(DbCommunity community, List<DbCommunityAgent>)>> GetAllWithAgentsAsync()
    {
        // Загружаем сообщества с их агентами
        var communities = await _provider.Communities
            .Include(c => c.Agents) // Явно включаем связанных агентов
            .ToListAsync();

        // Преобразуем в кортежи (сообщество, список агентов)
        return communities
            .Select(c => (Community: c, Agents: c.Agents.ToList()))
            .ToList();


    }
}
