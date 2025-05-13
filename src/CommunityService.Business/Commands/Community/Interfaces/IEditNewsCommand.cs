using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityHelper.CommunityService.Models.Dto.Requests.News;
using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;

namespace UniversityHelper.CommunityService.Business.Commands.Community.Interfaces;
[AutoInject]
public interface IEditNewsCommand
{
    Task<OperationResultResponse<bool>> ExecuteAsync(Guid newsId, JsonPatchDocument<EditNewsRequest> request);
}
