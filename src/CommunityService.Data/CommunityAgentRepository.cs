using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using UniversityHelper.CommunityService.Data.Interfaces;
using UniversityHelper.CommunityService.Data.Provider;
using UniversityHelper.CommunityService.Models.Db;

namespace UniversityHelper.CommunityService.Data;

public class CommunityAgentRepository : ICommunityAgentRepository
{
    private readonly IDataProvider _provider;

    public CommunityAgentRepository(IDataProvider provider)
    {
        _provider = provider;
    }

    public async Task CreateAsync(DbCommunityAgent agent)
    {
        await _provider.Agents.AddAsync(agent);
        await _provider.SaveAsync();
    }

    public async Task<bool> IsAgentAsync(Guid userId, Guid communityId)
    {
        return await _provider.Agents.AnyAsync(a => a.AgentId == userId && a.CommunityId == communityId);
    }

    public async Task<bool> IsModeratorAsync(Guid userId, Guid communityId)
    {
        return await _provider.Agents.AnyAsync(a => a.AgentId == userId && a.CommunityId == communityId);
    }

    public async Task RemoveAgentAsync(Guid communityId, Guid userId)
    {
        var agent = await _provider.Agents.FirstOrDefaultAsync(a => a.AgentId == userId && a.CommunityId == communityId);
        if (agent != null)
        {
            _provider.Agents.Remove(agent);
            await _provider.SaveAsync();
        }
    }
}