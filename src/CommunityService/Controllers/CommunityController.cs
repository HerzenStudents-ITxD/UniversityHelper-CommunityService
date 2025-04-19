using UniversityHelper.Core.Responses;
using UniversityHelper.CommunityService.Business.Commands.Community.Interfaces;
using UniversityHelper.CommunityService.Models.Dto.Requests.Community;
using UniversityHelper.CommunityService.Models.Dto.Requests.News;
using UniversityHelper.CommunityService.Models.Dto.Responses.Community;
using UniversityHelper.CommunityService.Models.Dto.Responses.News;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace UniversityHelper.CommunityService.Controllers;

[ApiController]
[Route("[controller]")]
public class CommunityController : ControllerBase
{
    [HttpGet("all")]
    public async Task<FindResultResponse<CommunityResponse>> GetAllCommunitiesAsync(
        [FromServices] IGetAllCommunitiesCommand command,
        [FromQuery] CancellationToken cancellationToken)
    {
        return await command.ExecuteAsync(cancellationToken);
    }

    [HttpGet("user")]
    public async Task<FindResultResponse<CommunityResponse>> GetUserCommunitiesAsync(
        [FromServices] IGetUserCommunitiesCommand command,
        [FromQuery] CancellationToken cancellationToken)
    {
        return await command.ExecuteAsync(cancellationToken);
    }

    [HttpPost("create")]
    public async Task<OperationResultResponse<Guid>> CreateCommunityAsync(
        [FromServices] ICreateCommunityCommand command,
        [FromBody] CreateCommunityRequest request)
    {
        return await command.ExecuteAsync(request);
    }

    [HttpPatch("edit")]
    public async Task<OperationResultResponse<bool>> EditCommunityAsync(
        [FromServices] IEditCommunityCommand command,
        [FromQuery] Guid communityId,
        [FromBody] JsonPatchDocument<EditCommunityRequest> request)
    {
        return await command.ExecuteAsync(communityId, request);
    }

    [HttpDelete("softdelete")]
    public async Task<OperationResultResponse<bool>> SoftDeleteCommunityAsync(
        [FromServices] ISoftDeleteCommunityCommand command,
        [FromQuery] Guid communityId)
    {
        return await command.ExecuteAsync(communityId);
    }

    [HttpPost("add-agent")]
    public async Task<OperationResultResponse<bool>> AddAgentAsync(
        [FromServices] IAddAgentCommand command,
        [FromBody] AddAgentRequest request)
    {
        return await command.ExecuteAsync(request);
    }

    [HttpDelete("remove-agent")]
    public async Task<OperationResultResponse<bool>> RemoveAgentAsync(
        [FromServices] IRemoveAgentCommand command,
        [FromQuery] Guid communityId,
        [FromQuery] Guid userId)
    {
        return await command.ExecuteAsync(communityId, userId);
    }

    [HttpGet("news")]
    public async Task<FindResultResponse<NewsResponse>> GetNewsAsync(
        [FromServices] IGetNewsCommand command,
        [FromQuery] int page,
        [FromQuery] int pageSize,
        [FromQuery] CancellationToken cancellationToken)
    {
        return await command.ExecuteAsync(page, pageSize, cancellationToken);
    }

    [HttpGet("community-news")]
    public async Task<FindResultResponse<NewsResponse>> GetCommunityNewsAsync(
        [FromServices] IGetNewsCommand command,
        [FromQuery] Guid communityId,
        [FromQuery] int page,
        [FromQuery] int pageSize,
        [FromQuery] CancellationToken cancellationToken)
    {
        return await command.ExecuteAsync(page, pageSize, cancellationToken);
    }

    [HttpPost("hide")]
    public async Task<OperationResultResponse<bool>> HideCommunityAsync(
        [FromServices] IHideCommunityCommand command,
        [FromQuery] Guid communityId)
    {
        return await command.ExecuteAsync(communityId);
    }

    [HttpPost("unhide")]
    public async Task<OperationResultResponse<bool>> UnhideCommunityAsync(
        [FromServices] IUnhideCommunityCommand command,
        [FromQuery] Guid communityId)
    {
        return await command.ExecuteAsync(communityId);
    }

    [HttpPost("participate")]
    public async Task<OperationResultResponse<bool>> ParticipateAsync(
        [FromServices] IParticipateCommand command,
        [FromBody] ParticipateRequest request)
    {
        return await command.ExecuteAsync(request.NewsId);
    }

    [HttpPost("unparticipate")]
    public async Task<OperationResultResponse<bool>> UnparticipateAsync(
        [FromServices] IUnparticipateCommand command,
        [FromBody] ParticipateRequest request)
    {
        return await command.ExecuteAsync(request.NewsId);
    }

    [HttpPost("create-news")]
    public async Task<OperationResultResponse<Guid>> CreateNewsAsync(
        [FromServices] ICreateNewsCommand command,
        [FromBody] CreateNewsRequest request)
    {
        return await command.ExecuteAsync(request);
    }
}