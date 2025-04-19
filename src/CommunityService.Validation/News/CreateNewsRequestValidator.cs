using FluentValidation;
using UniversityHelper.CommunityService.Models.Dto.Requests.News;
using UniversityHelper.CommunityService.Validation.News.Interfaces;

namespace UniversityHelper.CommunityService.Validation.News;

public class CreateNewsRequestValidator : AbstractValidator<CreateNewsRequest>, ICreateNewsRequestValidator
{
    public CreateNewsRequestValidator()
    {
        RuleFor(x => x.CommunityId)
          .NotEmpty().WithMessage("Community ID is required.");

        RuleFor(x => x.Title)
          .NotEmpty().WithMessage("Title is required.")
          .MaximumLength(200).WithMessage("Title is too long.");

        RuleFor(x => x.Content)
          .NotEmpty().WithMessage("Content is required.");
    }
}