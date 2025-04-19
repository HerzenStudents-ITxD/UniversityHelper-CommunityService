using UniversityHelper.Core.BrokerSupport.Broker;
using UniversityHelper.CommunityService.Data.Interfaces;
using MassTransit;
using UniversityHelper.Models.Broker.Requests.Community;

namespace UniversityHelper.CommunityService.Broker.Consumers;

public class CheckCommunityAccessConsumer : IConsumer<ICheckCommunityAccessRequest>
{
    private readonly ICommunityAgentRepository _agentRepository;

    public CheckCommunityAccessConsumer(ICommunityAgentRepository agentRepository)
    {
        _agentRepository = agentRepository;
    }

    public async Task Consume(ConsumeContext<ICheckCommunityAccessRequest> context)
    {
        var response = OperationResultWrapper.CreateResponse(CheckAccessAsync, context.Message);

        await context.RespondAsync<IOperationResult<bool>>(response);
    }

    private async Task<object> CheckAccessAsync(ICheckCommunityAccessRequest request)
    {
        return await _agentRepository.IsModeratorAsync(request.UserId, request.CommunityId) ||
               await _agentRepository.IsAgentAsync(request.UserId, request.CommunityId);
    }
}