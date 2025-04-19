using FluentValidation;
using UniversityHelper.Core.Attributes;
using UniversityHelper.CommunityService.Models.Dto.Requests.News;

namespace UniversityHelper.CommunityService.Validation.News.Interfaces;

[AutoInject]
public interface ICreateNewsRequestValidator : IValidator<CreateNewsRequest>
{
}