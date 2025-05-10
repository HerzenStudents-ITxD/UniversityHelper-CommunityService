using FluentValidation.Results;
using UniversityHelper.Core.Helpers.Interfaces;
using UniversityHelper.Core.Responses;
using UniversityHelper.CommunityService.Data.Interfaces;
using UniversityHelper.CommunityService.Mappers.Db.Interfaces;
using UniversityHelper.CommunityService.Models.Dto.Requests.News;
using UniversityHelper.CommunityService.Validation.News.Interfaces;
using UniversityHelper.CommunityService.Business.Commands.Community.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Net;
using System;
using UniversityHelper.Core.Extensions;

namespace UniversityHelper.CommunityService.Business.Commands.Community;

public class CreateNewsCommand : ICreateNewsCommand
{
    private readonly ICommunityNewsRepository _newsRepository;
    private readonly ICommunityNewsPhotoRepository _imageRepository;
    private readonly ICommunityAgentRepository _agentRepository;
    private readonly ICreateNewsRequestValidator _requestValidator;
    private readonly IDbCommunityNewsMapper _dbNewsMapper;
    private readonly IDbCommunityNewsPhotoMapper _dbImageMapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IResponseCreator _responseCreator;

    public CreateNewsCommand(
      ICommunityNewsRepository newsRepository,
      ICommunityNewsPhotoRepository imageRepository,
      ICommunityAgentRepository agentRepository,
      ICreateNewsRequestValidator requestValidator,
      IDbCommunityNewsMapper dbNewsMapper,
      IDbCommunityNewsPhotoMapper dbImageMapper,
      IHttpContextAccessor httpContextAccessor,
      IResponseCreator responseCreator)
    {
        _newsRepository = newsRepository;
        _imageRepository = imageRepository;
        _agentRepository = agentRepository;
        _requestValidator = requestValidator;
        _dbNewsMapper = dbNewsMapper;
        _dbImageMapper = dbImageMapper;
        _httpContextAccessor = httpContextAccessor;
        _responseCreator = responseCreator;
    }

    public async Task<OperationResultResponse<Guid>> ExecuteAsync(CreateNewsRequest request)
    {
        ValidationResult validationResult = await _requestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return _responseCreator.CreateFailureResponse<Guid>(
              HttpStatusCode.BadRequest,
              validationResult.Errors.Select(v => v.ErrorMessage).ToList());
        }

        var userId = _httpContextAccessor.HttpContext.GetUserId();
        if (!await _accessValidator.IsAdminAsync() && !await _agentRepository.IsAgentAsync(userId, request.CommunityId))
        {
            return _responseCreator.CreateFailureResponse<Guid>(HttpStatusCode.Forbidden, new List<string> { "User is not a agent or admin." });
        }

        var news = _dbNewsMapper.Map(request, userId);
        await _newsRepository.CreateAsync(news);

        // Обработка изображения, если оно предоставлено
        if (request.Image != null)
        {
            var image = _dbImageMapper.Map(request.Image, news.Id);
            await _imageRepository.CreateAsync(image);
        }

        OperationResultResponse<Guid> response = new() { Body = news.Id };

        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;
        return response;
    }
}