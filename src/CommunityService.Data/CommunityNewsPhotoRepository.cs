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

public class CommunityNewsPhotoRepository : ICommunityNewsPhotoRepository
{
    private readonly IDataProvider _provider;

    public CommunityNewsPhotoRepository(IDataProvider provider)
    {
        _provider = provider;
    }

    public async Task CreateAsync(DbNewsPhoto image)
    {
        await _provider.NewsPhotos.AddAsync(image);
        await _provider.SaveAsync();
    }

    public async Task<List<Guid>> GetImagesByNewsId(Guid newsId, CancellationToken cancellationToken)
    {
        return await _provider.NewsPhotos
            .Where(p => p.NewsId == newsId)
            .Select(p => p.Id)
            .ToListAsync(cancellationToken);
    }
}