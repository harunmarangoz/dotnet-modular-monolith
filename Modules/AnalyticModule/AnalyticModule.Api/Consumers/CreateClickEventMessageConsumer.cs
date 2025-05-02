using AnalyticModule.Application.ClickEventRequests.Commands;
using MassTransit;
using MediatR;
using Shared.Contracts.Messages;

namespace AnalyticModule.Api.Consumers;

public class CreateClickEventMessageConsumer(ISender sender) : IConsumer<CreateClickEventMessage>
{
    public async Task Consume(ConsumeContext<CreateClickEventMessage> context)
    {
        await sender.Send(new CreateClickEventCommand(new CreateClickEventCommandParameter
        {
            LinkId = context.Message.LinkId,
            LinkUniqueKey = context.Message.LinkUniqueKey,
            UserAgent = context.Message.UserAgent,
            IpAddress = context.Message.IpAddress
        }));
    }
}