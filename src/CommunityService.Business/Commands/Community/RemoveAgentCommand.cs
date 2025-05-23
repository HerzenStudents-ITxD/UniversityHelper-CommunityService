﻿using UniversityHelper.Core.Responses;
using UniversityHelper.CommunityService.Data.Interfaces;
using UniversityHelper.CommunityService.Business.Commands.Community.Interfaces;
using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using UniversityHelper.Core.RedisSupport.Helpers.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Net;
using UniversityHelper.Core.Extensions;
using UniversityHelper.Core.Helpers.Interfaces;

namespace UniversityHelper.CommunityService.Business.Commands.Community;

public class RemoveAgentCommand : IRemoveAgentCommand
{
    private readonly ICommunityAgentRepository _agentRepository;
    private readonly ICommunityRepository _communityRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IGlobalCacheRepository _globalCache;
    private readonly IResponseCreator _responseCreator;
    private readonly IAccessValidator _accessValidator;

    public RemoveAgentCommand(
      ICommunityAgentRepository agentRepository,
      ICommunityRepository communityRepository,
      IHttpContextAccessor httpContextAccessor,
      IGlobalCacheRepository globalCache,
      IAccessValidator accessValidator,
      IResponseCreator responseCreator)
    {
        _agentRepository = agentRepository;
        _communityRepository = communityRepository;
        _httpContextAccessor = httpContextAccessor;
        _globalCache = globalCache;
        _accessValidator = accessValidator;
        _responseCreator = responseCreator;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(Guid communityId, Guid userId)
    {
        var currentUserId = _httpContextAccessor.HttpContext.GetUserId();
        if (!await _accessValidator.IsAdminAsync() && !await _agentRepository.IsAgentAsync(currentUserId, communityId))
        {
            return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden, new List<string> { "User is not moderator or admin." });
        }

        var community = await _communityRepository.GetAsync(communityId, CancellationToken.None);
        if (community == null)
        {
            return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.NotFound, new List<string> { "Community not found." });
        }

        if (!await _agentRepository.IsAgentAsync(userId, communityId))
        {
            return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.NotFound, new List<string> { "User is not an agent." });
        }

        await _agentRepository.RemoveAgentAsync(communityId, userId);
        await _globalCache.RemoveAsync(communityId);

        return new OperationResultResponse<bool> { Body = true };
    }
}