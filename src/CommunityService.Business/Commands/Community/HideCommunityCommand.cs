﻿using UniversityHelper.Core.Responses;
using UniversityHelper.CommunityService.Data.Interfaces;
using UniversityHelper.CommunityService.Models.Db;
using UniversityHelper.CommunityService.Business.Commands.Community.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Net;
using UniversityHelper.Core.Extensions;
using UniversityHelper.Core.Helpers.Interfaces;

namespace UniversityHelper.CommunityService.Business.Commands.Community;

public class HideCommunityCommand : IHideCommunityCommand
{
    private readonly IHiddenCommunityRepository _hiddenCommunityRepository;
    private readonly ICommunityRepository _communityRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IResponseCreator _responseCreator;

    public HideCommunityCommand(
      IHiddenCommunityRepository hiddenCommunityRepository,
      ICommunityRepository communityRepository,
      IHttpContextAccessor httpContextAccessor,
      IResponseCreator responseCreator)
    {
        _hiddenCommunityRepository = hiddenCommunityRepository;
        _communityRepository = communityRepository;
        _httpContextAccessor = httpContextAccessor;
        _responseCreator = responseCreator;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(Guid communityId)
    {
        var userId = _httpContextAccessor.HttpContext.GetUserId();
        var community = await _communityRepository.GetAsync(communityId, CancellationToken.None);
        if (community == null)
        {
            return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.NotFound, new List<string> { "Community not found." });
        }

        if (await _hiddenCommunityRepository.IsCommunityHiddenAsync(userId, communityId))
        {
            return new OperationResultResponse<bool> { Body = true };
        }

        var hiddenCommunity = new DbCommunityHidden
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            CommunityId = communityId
        };

        await _hiddenCommunityRepository.AddHiddenCommunityAsync(hiddenCommunity);
        return new OperationResultResponse<bool> { Body = true };
    }
}