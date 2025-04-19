using UniversityHelper.Core.Responses;
using UniversityHelper.CommunityService.Business.Commands.Community.Interfaces;
using UniversityHelper.CommunityService.Data.Interfaces;
using UniversityHelper.CommunityService.Mappers.Responses.Interfaces;
using UniversityHelper.CommunityService.Models.Dto.Responses.Community;
using Microsoft.AspNetCore.Http;
using UniversityHelper.Core.Extensions;

namespace UniversityHelper.CommunityService.Business.Commands.Community;

public class GetUserCommunitiesCommand : IGetUserCommunitiesCommand
{
    private readonly ICommunityRepository _communityRepository;
    private readonly ICommunityResponseMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetUserCommunitiesCommand(
      ICommunityRepository communityRepository,
      ICommunityResponseMapper mapper,
      IHttpContextAccessor httpContextAccessor)
    {
        _communityRepository = communityRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<FindResultResponse<CommunityResponse>> ExecuteAsync(CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext.GetUserId();
        var (communities, totalCount) = await _communityRepository.FindAsync(
          userId, false, true, cancellationToken);

        var response = new FindResultResponse<CommunityResponse>
        {
            TotalCount = totalCount,
            Body = new List<CommunityResponse>()
        };


        return response;
    }
}