using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityHelper.CommunityService.Models.Dto.Requests.Community;
using UniversityHelper.CommunityService.Models.Dto.Requests.News;
using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;

namespace UniversityHelper.CommunityService.Business.Commands.Community.Interfaces;
[AutoInject]
public interface ICreateNewsCommand
{
    Task<OperationResultResponse<Guid>> ExecuteAsync(CreateNewsRequest request);
}
