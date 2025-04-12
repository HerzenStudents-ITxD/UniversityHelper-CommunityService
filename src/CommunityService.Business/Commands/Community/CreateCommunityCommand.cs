using FluentValidation.Results;
using StackExchange.Redis;
using System.Net;
using System.Threading.Tasks;
using UniversityHelper.CommunityService.Business.Commands.Community.Interfaces;
using UniversityHelper.CommunityService.Data.Interfaces;
using UniversityHelper.CommunityService.Models.Db;
using UniversityHelper.CommunityService.Models.Dto.Requests.Community;
using UniversityHelper.CommunityService.Validation.Community;
using UniversityHelper.CommunityService.Validation.Interfaces;
using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine;
using UniversityHelper.Core.BrokerSupport.AccessValidatorEngine.Interfaces;
using UniversityHelper.Core.Helpers.Interfaces;
using UniversityHelper.Core.Responses;

namespace UniversityHelper.CommunityService.Business.Commands.Community;

public class CreateCommunityCommand : ICreateCommunityCommand
{
    private readonly ICreateCommunityValidator _validator;
    private readonly IAccessValidator _accessValidator;
    private readonly ICommunityRepository _communityRepository;
    private readonly IResponseCreator _responseCreator;

    public CreateCommunityCommand(
        ICommunityRepository communityRepository,
        ICreateCommunityValidator validator,
        IAccessValidator accessValidator,
        IResponseCreator responseCreator)
    {
        _validator = validator;
        _accessValidator = accessValidator;
        _communityRepository = communityRepository;
        _responseCreator = responseCreator;
    }

    public async Task<OperationResultResponse<Guid>> ExecuteAsync(CreateCommunityRequest request)
    {
        //if (!await _accessValidator.IsAdminAsync())
        //{
        //    return _responseCreator.CreateFailureResponse<Guid>(HttpStatusCode.Forbidden);
        //}
        ValidationResult validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return _responseCreator.CreateFailureResponse<Guid>(
              HttpStatusCode.BadRequest,
              validationResult.Errors.Select(validationFailure => validationFailure.ErrorMessage).ToList());
        }
        var community = new DbCommunity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Avatar = request.Avatar
        };

        await _communityRepository.CreateAsync(community);
        return new OperationResultResponse<Guid>(body: community.Id);
    }
}
