using AnalyticModule.Application.ClickEventRequests.Commands;
using AnalyticModule.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AnalyticModule.Infrastructure.ClickEventRequests.Commands;

public class CreateClickEventCommandHandler(IDbContextFactory<AnalyticModuleDatabaseContext> contextFactory)
    : IRequestHandler<CreateClickEventCommand>
{
    public async Task Handle(CreateClickEventCommand request, CancellationToken cancellationToken)
    {
        var context = await contextFactory.CreateDbContextAsync(cancellationToken);

        var clickEvent = new Domain.Entities.ClickEvent
        {
            Id = Guid.NewGuid(),
            LinkId = request.Parameter.LinkId,
            LinkUniqueKey = request.Parameter.LinkUniqueKey,
            UserAgent = request.Parameter.UserAgent,
            IpAddress = request.Parameter.IpAddress,
            OccurredAt = DateTime.UtcNow
        };

        context.ClickEvents.Add(clickEvent);
        await context.SaveChangesAsync(cancellationToken);
    }
}