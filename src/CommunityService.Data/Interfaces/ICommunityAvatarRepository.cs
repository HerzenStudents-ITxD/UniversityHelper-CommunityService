using UniversityHelper.CommunityService.Models.Db;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;

namespace UniversityHelper.CommunityService.Data.Interfaces;
[AutoInject]
public interface ICommunityAvatarRepository
{
    Task<List<string>> GetAvatarsByCommunityId(Guid communityId, CancellationToken cancellationToken);
    Task<bool> UpdateCurrentAvatarAsync(Guid communityId, string avatar);
}