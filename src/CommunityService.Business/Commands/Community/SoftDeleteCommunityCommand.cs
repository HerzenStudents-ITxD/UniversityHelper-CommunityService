﻿using UniversityHelper.Core.Helpers.Interfaces;
using UniversityHelper.Core.RedisSupport.Helpers.Interfaces;
using UniversityHelper.Core.Responses;
using UniversityHelper.CommunityService.Business.Commands.Community.Interfaces;
using UniversityHelper.CommunityService.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Net;
using UniversityHelper.Core.Extensions;

namespace UniversityHelper.CommunityService.Business.Commands.Community;

public class SoftDeleteCommunityCommand : ISoftDeleteCommunityCommand
{
    private readonly ICommunityRepository _communityRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IResponseCreator _responseCreator;
    private readonly IGlobalCacheRepository _globalCache;

    public SoftDeleteCommunityCommand(
      ICommunityRepository communityRepository,
      IHttpContextAccessor httpContextAccessor,
      IResponseCreator responseCreator,
      IGlobalCacheRepository globalCache)
    {
        _communityRepository = communityRepository;
        _httpContextAccessor = httpContextAccessor;
        _responseCreator = responseCreator;
        _globalCache = globalCache;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(Guid communityId)
    {
        var userId = _httpContextAccessor.HttpContext.GetUserId();
        // Assuming admin check is done via broker or middleware
        var success = await _communityRepository.SoftDeleteAsync(communityId);

        if (success)
        {
            await _globalCache.RemoveAsync(communityId);
        }
        else
        {
            return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.NotFound);
        }

        return new OperationResultResponse<bool> { Body = success };
    }
}