using UniversityHelper.Core.Responses;
using UniversityHelper.CommunityService.Business.Commands.Community.Interfaces;
using UniversityHelper.CommunityService.Data.Interfaces;
using UniversityHelper.CommunityService.Mappers.Responses.Interfaces;
using UniversityHelper.CommunityService.Models.Dto.Responses.Community;
using Microsoft.AspNetCore.Http;
using UniversityHelper.Core.Extensions;

namespace UniversityHelper.CommunityService.Business.Commands.Community;

public class GetAllCommunitiesCommand : IGetAllCommunitiesCommand
{
    private readonly ICommunityRepository _communityRepository;
    private readonly ICommunityResponseMapper _mapper;

    public GetAllCommunitiesCommand(
      ICommunityRepository communityRepository,
      ICommunityResponseMapper mapper)
    {
        _communityRepository = communityRepository;
        _mapper = mapper;
    }

    public async Task<FindResultResponse<CommunityResponse>> ExecuteAsync(CancellationToken cancellationToken)
    {
        var (communities, totalCount) = await _communityRepository.FindAsync(
          null, true, true, cancellationToken);

        var response = new FindResultResponse<CommunityResponse>
        {
            TotalCount = totalCount,
            Body = new List<CommunityResponse>()
        };


        return response;
    }
}