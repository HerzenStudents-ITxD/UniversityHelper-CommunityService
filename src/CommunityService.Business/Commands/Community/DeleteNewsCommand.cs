using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UniversityHelper.CommunityService.Business.Commands.Community.Interfaces;
using UniversityHelper.CommunityService.Data.Interfaces;
using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using UniversityHelper.Core.Extensions;
using UniversityHelper.Core.Helpers.Interfaces;
using UniversityHelper.Core.Responses;

namespace UniversityHelper.CommunityService.Business.Commands.Community;
public class DeleteNewsCommand : IDeleteNewsCommand
{
    private readonly ICommunityNewsRepository _newsRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IResponseCreator _responseCreator;
    private readonly IAccessValidator _accessValidator;

    public DeleteNewsCommand(
        ICommunityNewsRepository newsRepository,
        IHttpContextAccessor httpContextAccessor,
        IResponseCreator responseCreator,
        IAccessValidator accessValidator)
    {
        _newsRepository = newsRepository;
        _httpContextAccessor = httpContextAccessor;
        _responseCreator = responseCreator;
        _accessValidator = accessValidator;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(Guid newsId)
    {
        var userId = _httpContextAccessor.HttpContext.GetUserId();
        var news = await _newsRepository.GetAsync(newsId, CancellationToken.None);
        if (news == null)
            return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.NotFound);

        if (!await _accessValidator.IsAdminAsync() && news.AuthorId != userId)
            return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden);

        return new OperationResultResponse<bool> { Body = await _newsRepository.DeleteAsync(newsId) };
    }
}
