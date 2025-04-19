using UniversityHelper.CommunityService.Models.Db;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;

namespace UniversityHelper.CommunityService.Data.Interfaces;
[AutoInject]
public interface ICommunityNewsPhotoRepository
{
    Task CreateAsync(DbNewsPhoto image);
    Task<List<Guid>> GetImagesByNewsId(Guid newsId, CancellationToken cancellationToken);
}