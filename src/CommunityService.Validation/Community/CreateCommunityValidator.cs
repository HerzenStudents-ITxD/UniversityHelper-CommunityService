﻿using FluentValidation;
using UniversityHelper.CommunityService.Models.Dto.Requests.Community;
using UniversityHelper.CommunityService.Validation.Community.Interfaces;

namespace UniversityHelper.CommunityService.Validation.Community;

public class CreateCommunityRequestValidator : AbstractValidator<CreateCommunityRequest>, ICreateCommunityRequestValidator
{
    public CreateCommunityRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Community name is required.")
            .MaximumLength(100).WithMessage("Community name is too long.");

        // Убрали валидацию для AvatarImage, так как оно теперь необязательное
        // Text также необязательное, поэтому валидация не требуется
    }
}