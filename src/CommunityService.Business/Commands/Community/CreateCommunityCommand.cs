using FluentValidation.Results;
using UniversityHelper.Core.Helpers.Interfaces;
using UniversityHelper.Core.RedisSupport.Helpers.Interfaces;
using UniversityHelper.Core.Responses;
using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using UniversityHelper.CommunityService.Business.Commands.Community.Interfaces;
using UniversityHelper.CommunityService.Data.Interfaces;
using UniversityHelper.CommunityService.Mappers.Db.Interfaces;
using UniversityHelper.CommunityService.Models.Dto.Requests.Community;
using UniversityHelper.CommunityService.Validation.Community.Interfaces;
using Microsoft.AspNetCore.Http;
using UniversityHelper.Core.Extensions;
using System.Net;

namespace UniversityHelper.CommunityService.Business.Commands.Community;

public class CreateCommunityCommand : ICreateCommunityCommand
{
    private readonly ICommunityRepository _communityRepository;
    private readonly ICommunityAvatarRepository _avatarRepository;
    private readonly ICreateCommunityRequestValidator _requestValidator;
    private readonly IDbCommunityMapper _dbCommunityMapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IResponseCreator _responseCreator;
    private readonly IGlobalCacheRepository _globalCache;
    private readonly IAccessValidator _accessValidator;

    public CreateCommunityCommand(
      ICommunityRepository communityRepository,
      ICommunityAvatarRepository avatarRepository,
      ICreateCommunityRequestValidator requestValidator,
      IDbCommunityMapper dbCommunityMapper,
      IAccessValidator accessValidator,
      IHttpContextAccessor httpContextAccessor,
      IResponseCreator responseCreator,
      IGlobalCacheRepository globalCache)
    {
        _communityRepository = communityRepository;
        _avatarRepository = avatarRepository;
        _requestValidator = requestValidator;
        _dbCommunityMapper = dbCommunityMapper;
        _accessValidator = accessValidator;
        _httpContextAccessor = httpContextAccessor;
        _responseCreator = responseCreator;
        _globalCache = globalCache;
    }

    public async Task<OperationResultResponse<Guid>> ExecuteAsync(CreateCommunityRequest request)
    {
        ValidationResult validationResult = await _requestValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return _responseCreator.CreateFailureResponse<Guid>(
              HttpStatusCode.BadRequest,
              validationResult.Errors.Select(v => v.ErrorMessage).ToList());
        }

        var userId = _httpContextAccessor.HttpContext.GetUserId();
        if (!await _accessValidator.IsAdminAsync())
        {
            return _responseCreator.CreateFailureResponse<Guid>(HttpStatusCode.Forbidden, new List<string> { "User is not a admin." });
        }

        var community = _dbCommunityMapper.Map(request);
        await _communityRepository.CreateAsync(community);

        OperationResultResponse<Guid> response = new() { Body = community.Id };


        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;
        await _globalCache.RemoveAsync(community.Id);

        return response;
    }
}