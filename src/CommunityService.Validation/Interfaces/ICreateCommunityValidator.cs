using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityHelper.CommunityService.Models.Dto.Requests.Community;
using UniversityHelper.Core.Attributes;

namespace UniversityHelper.CommunityService.Validation.Interfaces;

[AutoInject]
public interface ICreateCommunityValidator : IValidator<CreateCommunityRequest>
{
}
