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
public class NewsController : ControllerBase
{
    [HttpGet("news")]
    [ProducesResponseType(typeof(FindResultResponse<NewsResponse>), StatusCodes.Status200OK)]
    public async Task<FindResultResponse<NewsResponse>> GetNewsAsync(
        [FromServices] IGetNewsCommand command,
        [FromQuery] int page,
        [FromQuery] int pageSize,
        [FromQuery] CancellationToken cancellationToken)
    {
        return await command.ExecuteAsync(page, pageSize, cancellationToken);
    }

    [HttpGet("community-news")]
    [ProducesResponseType(typeof(FindResultResponse<NewsResponse>), StatusCodes.Status200OK)]
    public async Task<FindResultResponse<NewsResponse>> GetCommunityNewsAsync(
        [FromServices] IGetNewsCommand command,
        [FromQuery] Guid communityId,
        [FromQuery] int page,
        [FromQuery] int pageSize,
        [FromQuery] CancellationToken cancellationToken)
    {
        return await command.ExecuteAsync(page, pageSize, cancellationToken);
    }

    [HttpPost("participate")]
    [ProducesResponseType(typeof(OperationResultResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<OperationResultResponse<bool>> ParticipateAsync(
        [FromServices] IParticipateCommand command,
        [FromBody] ParticipateRequest request)
    {
        return await command.ExecuteAsync(request.NewsId);
    }

    [HttpPost("unparticipate")]
    [ProducesResponseType(typeof(OperationResultResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<OperationResultResponse<bool>> UnparticipateAsync(
        [FromServices] IUnparticipateCommand command,
        [FromBody] ParticipateRequest request)
    {
        return await command.ExecuteAsync(request.NewsId);
    }

    [HttpPost("create-news")]
    [ProducesResponseType(typeof(OperationResultResponse<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<OperationResultResponse<Guid>> CreateNewsAsync(
        [FromServices] ICreateNewsCommand command,
        [FromBody] CreateNewsRequest request)
    {
        return await command.ExecuteAsync(request);
    }
}