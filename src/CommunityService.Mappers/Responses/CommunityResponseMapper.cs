using SixLabors.ImageSharp;
using UniversityHelper.CommunityService.Mappers.Responses.Interfaces;
using UniversityHelper.CommunityService.Models.Db;
using UniversityHelper.CommunityService.Models.Dto.Models;
using UniversityHelper.CommunityService.Models.Dto.Responses.Community;

namespace UniversityHelper.CommunityService.Mappers.Responses;

public class CommunityResponseMapper : ICommunityResponseMapper
{
    public CommunityResponse Map(DbCommunity community, List<string> images)
    {
        return new CommunityResponse
        {
            Community = new CommunityInfo
            {
                Id = community.Id,
                Name = community.Name,
                IsHidden = community.Hidden.Any(),
                Avatar = community.Avatar,
            },
            Agents = community.Agents?.Select(a => new CommunityAgentInfo
            {
                UserId = a.AgentId,
            }).ToList() ?? new List<CommunityAgentInfo>()
        };
    }
}