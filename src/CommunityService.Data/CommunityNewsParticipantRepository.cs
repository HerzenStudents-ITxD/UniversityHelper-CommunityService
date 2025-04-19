using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using UniversityHelper.CommunityService.Data.Interfaces;
using UniversityHelper.CommunityService.Data.Provider;
using UniversityHelper.CommunityService.Models.Db;

namespace UniversityHelper.CommunityService.Data;

public class CommunityNewsParticipantRepository : ICommunityNewsParticipantRepository
{
    private readonly IDataProvider _provider;

    public CommunityNewsParticipantRepository(IDataProvider provider)
    {
        _provider = provider;
    }

    public async Task AddParticipantAsync(DbParticipating participant)
    {
        await _provider.Participating.AddAsync(participant);
        await _provider.SaveAsync();
    }

    public async Task RemoveParticipantAsync(Guid newsId, Guid userId)
    {
        var participant = await _provider.Participating
            .FirstOrDefaultAsync(p => p.NewsId == newsId && p.UserId == userId);
        if (participant != null)
        {
            _provider.Participating.Remove(participant);
            await _provider.SaveAsync();
        }
    }

    public async Task<bool> IsParticipantAsync(Guid newsId, Guid userId)
    {
        return await _provider.Participating
            .AnyAsync(p => p.NewsId == newsId && p.UserId == userId);
    }
}