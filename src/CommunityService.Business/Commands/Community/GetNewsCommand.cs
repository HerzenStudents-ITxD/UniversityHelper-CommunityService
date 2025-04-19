using UniversityHelper.Core.Responses;
using UniversityHelper.CommunityService.Data.Interfaces;
using UniversityHelper.CommunityService.Mappers.Responses.Interfaces;
using UniversityHelper.CommunityService.Models.Dto.Responses.News;
using UniversityHelper.CommunityService.Business.Commands.Community.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;
using UniversityHelper.Core.Extensions;
using UniversityHelper.Core.Helpers.Interfaces;
using UniversityHelper.CommunityService.Mappers.Responses;

namespace UniversityHelper.CommunityService.Business.Commands.Community;

public class GetNewsCommand : IGetNewsCommand
{
    private readonly ICommunityNewsRepository _newsRepository;
    private readonly ICommunityNewsPhotoRepository _imageRepository;
    private readonly INewsResponseMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetNewsCommand(
        ICommunityNewsRepository newsRepository,
        ICommunityNewsPhotoRepository imageRepository,
        INewsResponseMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _newsRepository = newsRepository;
        _imageRepository = imageRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<FindResultResponse<NewsResponse>> ExecuteAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext.GetUserId();
        var (news, totalCount) = await _newsRepository.FindAsync(userId, page, pageSize, cancellationToken);

        var response = new FindResultResponse<NewsResponse>
        {
            TotalCount = totalCount,
            Body = new List<NewsResponse>()
        };


        return response;
    }
}