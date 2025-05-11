using UniversityHelper.Core.Helpers.Interfaces;
using UniversityHelper.Core.RedisSupport.Helpers.Interfaces;
using UniversityHelper.Core.Responses;
using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using UniversityHelper.CommunityService.Business.Commands.Community.Interfaces;
using UniversityHelper.CommunityService.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using System.Net;
using UniversityHelper.CommunityService.Models.Dto.Requests.Community;
using UniversityHelper.Core.Extensions;

namespace UniversityHelper.CommunityService.Business.Commands.Community;

public class EditCommunityCommand : IEditCommunityCommand
{
    private readonly ICommunityRepository _communityRepository;
    private readonly ICommunityAgentRepository _agentRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IResponseCreator _responseCreator;
    private readonly IGlobalCacheRepository _globalCache;
    private readonly IAccessValidator _accessValidator;

    public EditCommunityCommand(
      ICommunityRepository communityRepository,
      ICommunityAgentRepository agentRepository,
      IHttpContextAccessor httpContextAccessor,
      IResponseCreator responseCreator,
      IAccessValidator accessValidator,
      IGlobalCacheRepository globalCache)
    {
        _communityRepository = communityRepository;
        _agentRepository = agentRepository;
        _httpContextAccessor = httpContextAccessor;
        _responseCreator = responseCreator;
        _accessValidator = accessValidator;
        _globalCache = globalCache;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(Guid communityId, JsonPatchDocument<EditCommunityRequest> request)
    {
        var userId = _httpContextAccessor.HttpContext.GetUserId();
        if (!await _accessValidator.IsAdminAsync() && !await _agentRepository.IsAgentAsync(userId, communityId))
        {
            return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden);
        }

        var community = await _communityRepository.GetAsync(communityId, CancellationToken.None);
        if (community == null)
        {
            return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.NotFound);
        }

        var editRequest = new EditCommunityRequest();
        request.ApplyTo(editRequest);

        community.Name = editRequest.Name ?? community.Name;
        community.ModifiedBy = userId;
        community.ModifiedAtUtc = DateTime.UtcNow;

        var success = await _communityRepository.UpdateAsync(community);
        if (success)
        {
            await _globalCache.RemoveAsync(communityId);
        }

        return new OperationResultResponse<bool> { Body = success };
    }
}