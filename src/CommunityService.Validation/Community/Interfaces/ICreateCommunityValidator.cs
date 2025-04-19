using FluentValidation;
using UniversityHelper.Core.Attributes;
using UniversityHelper.CommunityService.Models.Dto.Requests.Community;

namespace UniversityHelper.CommunityService.Validation.Community.Interfaces;

[AutoInject]
public interface ICreateCommunityRequestValidator : IValidator<CreateCommunityRequest>
{
}