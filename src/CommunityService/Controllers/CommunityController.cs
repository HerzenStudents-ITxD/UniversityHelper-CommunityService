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
    [ProducesResponseType(typeof(FindResultResponse<CommunityResponse>), StatusCodes.Status200OK)]
    public async Task<FindResultResponse<CommunityResponse>> GetAllCommunitiesAsync(
        [FromServices] IGetAllCommunitiesCommand command,
        [FromQuery] CancellationToken cancellationToken)
    {
        return await command.ExecuteAsync(cancellationToken);
    }

    [HttpGet("user")]
    [ProducesResponseType(typeof(FindResultResponse<CommunityResponse>), StatusCodes.Status200OK)]
    public async Task<FindResultResponse<CommunityResponse>> GetUserCommunitiesAsync(
        [FromServices] IGetUserCommunitiesCommand command,
        [FromQuery] CancellationToken cancellationToken)
    {
        return await command.ExecuteAsync(cancellationToken);
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(OperationResultResponse<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<OperationResultResponse<Guid>> CreateCommunityAsync(
        [FromServices] ICreateCommunityCommand command,
        [FromBody] CreateCommunityRequest request)
    {
        return await command.ExecuteAsync(request);
    }

    [HttpPatch("edit")]
    [ProducesResponseType(typeof(OperationResultResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<OperationResultResponse<bool>> EditCommunityAsync(
        [FromServices] IEditCommunityCommand command,
        [FromQuery] Guid communityId,
        [FromBody] JsonPatchDocument<EditCommunityRequest> request)
    {
        return await command.ExecuteAsync(communityId, request);
    }

    [HttpDelete("softdelete")]
    [ProducesResponseType(typeof(OperationResultResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<OperationResultResponse<bool>> SoftDeleteCommunityAsync(
        [FromServices] ISoftDeleteCommunityCommand command,
        [FromQuery] Guid communityId)
    {
        return await command.ExecuteAsync(communityId);
    }

    [HttpPost("add-agent")]
    [ProducesResponseType(typeof(OperationResultResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<OperationResultResponse<bool>> AddAgentAsync(
        [FromServices] IAddAgentCommand command,
        [FromBody] AddAgentRequest request)
    {
        return await command.ExecuteAsync(request);
    }

    [HttpDelete("remove-agent")]
    [ProducesResponseType(typeof(OperationResultResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<OperationResultResponse<bool>> RemoveAgentAsync(
        [FromServices] IRemoveAgentCommand command,
        [FromQuery] Guid communityId,
        [FromQuery] Guid userId)
    {
        return await command.ExecuteAsync(communityId, userId);
    }

    [HttpPost("hide")]
    [ProducesResponseType(typeof(OperationResultResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<OperationResultResponse<bool>> HideCommunityAsync(
        [FromServices] IHideCommunityCommand command,
        [FromQuery] Guid communityId)
    {
        return await command.ExecuteAsync(communityId);
    }

    [HttpPost("unhide")]
    [ProducesResponseType(typeof(OperationResultResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<OperationResultResponse<bool>> UnhideCommunityAsync(
        [FromServices] IUnhideCommunityCommand command,
        [FromQuery] Guid communityId)
    {
        return await command.ExecuteAsync(communityId);
    }
}