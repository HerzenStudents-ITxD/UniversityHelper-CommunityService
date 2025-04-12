//using UniversityHelper.CommunityService.Business.Commands.Interfaces;
using UniversityHelper.CommunityService.Models.Dto.Requests;
using UniversityHelper.CommunityService.Models.Dto.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UniversityHelper.CommunityService.Business.Commands.Community.Interfaces;
using UniversityHelper.CommunityService.Models.Dto.Requests.Community;
using UniversityHelper.Core.Responses;

namespace UniversityHelper.CommunityService.Controllers;

[Route("[controller]")]
[ApiController]
public class CommunityController
{
  //[HttpGet("all")]
  //public async Task<OperationResultResponse<Guid>> LoginUser(
  //    //[FromServices] ILoginCommand command,
  //    [FromBody] LoginRequest userCredentials)
  //{
  //  return await command.Execute(userCredentials);
  //}

    [HttpPost("create")]
    public async Task<OperationResultResponse<Guid>> CreateCommunity(
              [FromServices] ICreateCommunityCommand command,
              [FromBody] CreateCommunityRequest request)
    {
        return await command.ExecuteAsync(request);
    }

    //[HttpPost("{communityId}/agents")]
    //public async Task<OperationResultResponse<bool>> AddAgent(
    //    [FromServices] IAddAgentCommand command,
    //    [FromRoute] Guid communityId,
    //    [FromBody] AddAgentRequest request)
    //{
    //    request.CommunityId = communityId;
    //    return await command.ExecuteAsync(request);
    //}


}
