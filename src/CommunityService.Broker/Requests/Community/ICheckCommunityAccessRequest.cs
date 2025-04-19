using System;

namespace UniversityHelper.Models.Broker.Requests.Community;

public interface ICheckCommunityAccessRequest
{
    Guid UserId { get; set; }
    Guid CommunityId { get; set; }
}