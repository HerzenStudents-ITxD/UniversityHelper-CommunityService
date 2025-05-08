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

    public GetNewsCommand(
        ICommunityNewsRepository newsRepository,
        ICommunityNewsPhotoRepository imageRepository)
    {
        _newsRepository = newsRepository;
        _imageRepository = imageRepository;
    }

    public async Task<FindResultResponse<NewsResponse>> ExecuteAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        var (news, totalCount) = await _newsRepository.FindAsync(page, pageSize, cancellationToken);

        var response = new FindResultResponse<NewsResponse>
        {
            TotalCount = totalCount,
            Body = new List<NewsResponse>()
        };


        return response;
    }
}