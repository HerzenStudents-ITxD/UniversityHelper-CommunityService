using FluentValidation;
using UniversityHelper.CommunityService.Models.Dto.Requests.Community;
using UniversityHelper.CommunityService.Validation.Interfaces;

namespace UniversityHelper.CommunityService.Validation.Community;

public class CreateCommunityValidator : AbstractValidator<CreateCommunityRequest>, ICreateCommunityValidator
{
    public CreateCommunityValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
    }
}
