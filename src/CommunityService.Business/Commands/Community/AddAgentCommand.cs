using UniversityHelper.Core.Responses;
using UniversityHelper.CommunityService.Data.Interfaces;
using UniversityHelper.CommunityService.Models.Db;
using UniversityHelper.CommunityService.Models.Dto.Requests.Community;
using UniversityHelper.CommunityService.Business.Commands.Community.Interfaces;
using UniversityHelper.Core.RedisSupport.Helpers.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Net;
using UniversityHelper.Core.Extensions;
using UniversityHelper.Core.Helpers.Interfaces;

namespace UniversityHelper.CommunityService.Business.Commands.Community;

public class AddAgentCommand : IAddAgentCommand
{
    private readonly ICommunityAgentRepository _agentRepository;
    private readonly ICommunityRepository _communityRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IGlobalCacheRepository _globalCache;
    private readonly IResponseCreator _responseCreator;

    public AddAgentCommand(
        ICommunityAgentRepository agentRepository,
        ICommunityRepository communityRepository,
        IHttpContextAccessor httpContextAccessor,
        IGlobalCacheRepository globalCache,
        IResponseCreator responseCreator)
    {
        _agentRepository = agentRepository;
        _communityRepository = communityRepository;
        _httpContextAccessor = httpContextAccessor;
        _globalCache = globalCache;
        _responseCreator = responseCreator;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(AddAgentRequest request)
    {
        var userId = _httpContextAccessor.HttpContext.GetUserId();
        if (!await _agentRepository.IsAgentAsync(userId, request.CommunityId) && !await _accessValidator.IsAdminAsync())
        {
            return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden, new List<string> { "User is not a moderator." });
        }

        var community = await _communityRepository.GetAsync(request.CommunityId, CancellationToken.None);
        if (community == null)
        {
            return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.NotFound, new List<string> { "Community not found." });
        }

        var agent = new DbCommunityAgent
        {
            Id = Guid.NewGuid(),
            CommunityId = request.CommunityId,
            AgentId = request.UserId,
        };

        await _agentRepository.CreateAsync(agent);
        await _globalCache.RemoveAsync(request.CommunityId);

        return new OperationResultResponse<bool> { Body = true };
    }
}