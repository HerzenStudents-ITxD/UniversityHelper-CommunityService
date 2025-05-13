using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UniversityHelper.CommunityService.Business.Commands.Community.Interfaces;
using UniversityHelper.CommunityService.Data.Interfaces;
using UniversityHelper.CommunityService.Models.Dto.Requests.News;
using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using UniversityHelper.Core.Extensions;
using UniversityHelper.Core.Helpers.Interfaces;
using UniversityHelper.Core.Responses;

namespace UniversityHelper.CommunityService.Business.Commands.Community;
public class EditNewsCommand : IEditNewsCommand
{
    private readonly ICommunityNewsRepository _newsRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IResponseCreator _responseCreator;
    private readonly IAccessValidator _accessValidator;

    public EditNewsCommand(
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

    public async Task<OperationResultResponse<bool>> ExecuteAsync(Guid newsId, JsonPatchDocument<EditNewsRequest> request)
    {
        var userId = _httpContextAccessor.HttpContext.GetUserId();
        var news = await _newsRepository.GetAsync(newsId, CancellationToken.None);
        if (news == null)
            return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.NotFound);

        if (!await _accessValidator.IsAdminAsync() && news.AuthorId != userId)
            return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden);

        var editRequest = new EditNewsRequest();
        request.ApplyTo(editRequest);

        if (editRequest.Title != null)
            news.Title = editRequest.Title;
        if (editRequest.Text != null)
            news.Text = editRequest.Text;
        if (editRequest.PointId != null)
            news.PointId = editRequest.PointId;
        // Images handling can be added if needed

        news.ModifiedBy = userId;
        news.ModifiedAtUtc = DateTime.UtcNow;

        await _newsRepository.UpdateAsync(news);
        return new OperationResultResponse<bool> { Body = true };
    }
}
