using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using UniversityHelper.CommunityService.Data.Interfaces;
using UniversityHelper.CommunityService.Data.Provider;
using UniversityHelper.CommunityService.Models.Db;

namespace UniversityHelper.CommunityService.Data;

public class HiddenCommunityRepository : IHiddenCommunityRepository
{
    private readonly IDataProvider _provider;

    public HiddenCommunityRepository(IDataProvider provider)
    {
        _provider = provider;
    }

    public async Task AddHiddenCommunityAsync(DbCommunityHidden hiddenCommunity)
    {
        await _provider.HiddenCommunities.AddAsync(hiddenCommunity);
        await _provider.SaveAsync();
    }

    public async Task RemoveHiddenCommunityAsync(Guid userId, Guid communityId)
    {
        var hidden = await _provider.HiddenCommunities
            .FirstOrDefaultAsync(h => h.UserId == userId && h.CommunityId == communityId);
        if (hidden != null)
        {
            _provider.HiddenCommunities.Remove(hidden);
            await _provider.SaveAsync();
        }
    }

    public async Task<bool> IsCommunityHiddenAsync(Guid userId, Guid communityId)
    {
        return await _provider.HiddenCommunities
            .AnyAsync(h => h.UserId == userId && h.CommunityId == communityId);
    }
}