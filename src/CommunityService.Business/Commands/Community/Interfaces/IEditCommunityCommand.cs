using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityHelper.CommunityService.Models.Dto.Requests.Community;
using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Responses;

namespace UniversityHelper.CommunityService.Business.Commands.Community.Interfaces;
[AutoInject]
public interface IEditCommunityCommand
{
    Task<OperationResultResponse<bool>> ExecuteAsync(Guid communityId, JsonPatchDocument<EditCommunityRequest> request);
}
