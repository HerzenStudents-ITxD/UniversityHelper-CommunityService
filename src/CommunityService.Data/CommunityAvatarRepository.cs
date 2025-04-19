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

public class CommunityAvatarRepository : ICommunityAvatarRepository
{
    private readonly IDataProvider _provider;

    public CommunityAvatarRepository(IDataProvider provider)
    {
        _provider = provider;
    }

    public async Task<List<string>> GetAvatarsByCommunityId(Guid communityId, CancellationToken cancellationToken)
    {
        var avatars = await _provider.Communities
            .Where(c => c.Id == communityId)
            .Select(c => c.Avatar)
            .ToListAsync(cancellationToken);

        return avatars.Where(a => !string.IsNullOrEmpty(a)).Distinct().ToList();
    }

    public async Task<bool> UpdateCurrentAvatarAsync(Guid communityId, string avatar)
    {
        var community = await _provider.Communities
            .FirstOrDefaultAsync(c => c.Id == communityId);

        if (community == null)
        {
            return false;
        }

        community.Avatar = avatar;
        _provider.Communities.Update(community);
        await _provider.SaveAsync();
        return true;
    }
}