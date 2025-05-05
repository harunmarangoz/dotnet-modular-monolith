using AnalyticModule.Application.ClickEventRequests.Queries;
using AnalyticModule.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Results;
using Shared.Application.Services;

namespace AnalyticModule.Infrastructure.ClickEventRequests.Queries;

public class ReportClickEventCommandHandler(
    IDbContextFactory<AnalyticModuleDatabaseContext> contextFactory,
    ILinkModuleService linkModuleService,
    IQrModuleService qrModuleService
) : IRequestHandler<ReportClickEventCommand, DataResult<ReportClickEventCommandResult>>
{
    public async Task<DataResult<ReportClickEventCommandResult>> Handle(ReportClickEventCommand request,
        CancellationToken cancellationToken)
    {
        var context = await contextFactory.CreateDbContextAsync(cancellationToken);

        var linkResult = await linkModuleService.GetLinkByIdAsync(request.LinkId, cancellationToken);
        if (linkResult.HasError) return DataResult<ReportClickEventCommandResult>.Failure(linkResult.Message);
        var link = linkResult.Data;

        var result = new ReportClickEventCommandResult()
        {
            LinkId = link.Id,
            LinkName = link.Name,
            LinkUrl = link.Url,
            LinkUniqueKey = link.UniqueKey
        };

        var lastClickEvent = await context.ClickEvents
            .OrderByDescending(x => x.OccurredAt)
            .FirstOrDefaultAsync(x => x.LinkId == link.Id, cancellationToken);

        if (lastClickEvent != null) result.LastClickDate = lastClickEvent.OccurredAt;

        var last7DaysClickCount = await context.ClickEvents
            .Where(x => x.LinkId == link.Id && x.OccurredAt >= DateTime.UtcNow.AddDays(-7))
            .CountAsync(cancellationToken);

        result.Last7DaysClickCount = last7DaysClickCount;

        var last30DaysClickCount = await context.ClickEvents
            .Where(x => x.LinkId == link.Id && x.OccurredAt >= DateTime.UtcNow.AddDays(-30))
            .CountAsync(cancellationToken);

        result.Last30DaysClickCount = last30DaysClickCount;

        var totalClickCount = await context.ClickEvents
            .Where(x => x.LinkId == link.Id)
            .CountAsync(cancellationToken);
        result.TotalClickCount = totalClickCount;

        var qrCodeResult = await qrModuleService.GetQrImageAsync(link.Id, cancellationToken);
        if (!qrCodeResult.HasError)
            result.QrBase64Image = qrCodeResult.Data;

        return DataResult<ReportClickEventCommandResult>.Success(result);
    }
}