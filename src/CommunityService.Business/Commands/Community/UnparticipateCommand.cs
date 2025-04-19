using UniversityHelper.Core.Responses;
using UniversityHelper.CommunityService.Data.Interfaces;
using UniversityHelper.CommunityService.Business.Commands.Community.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Net;
using UniversityHelper.Core.Extensions;
using UniversityHelper.Core.Helpers.Interfaces;

namespace UniversityHelper.CommunityService.Business.Commands.Community;

public class UnparticipateCommand : IUnparticipateCommand
{
    private readonly ICommunityNewsParticipantRepository _participantRepository;
    private readonly ICommunityNewsRepository _newsRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IResponseCreator _responseCreator;

    public UnparticipateCommand(
      ICommunityNewsParticipantRepository participantRepository,
      ICommunityNewsRepository newsRepository,
      IHttpContextAccessor httpContextAccessor,
      IResponseCreator responseCreator)
    {
        _participantRepository = participantRepository;
        _newsRepository = newsRepository;
        _httpContextAccessor = httpContextAccessor;
        _responseCreator = responseCreator;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(Guid newsId)
    {
        var userId = _httpContextAccessor.HttpContext.GetUserId();
        var news = await _newsRepository.GetAsync(newsId, CancellationToken.None);
        if (news == null)
        {
            return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.NotFound, new List<string> { "News not found." });
        }

        if (!await _participantRepository.IsParticipantAsync(newsId, userId))
        {
            return new OperationResultResponse<bool> { Body = true };
        }

        await _participantRepository.RemoveParticipantAsync(newsId, userId);
        return new OperationResultResponse<bool> { Body = true };
    }
}