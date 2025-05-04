using LinkModule.Application.LinkRequests.Commands;
using LinkModule.Domain.Entities;
using LinkModule.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LinkModule.Infrastructure.LinkRequests.Commands;

public class CreateLinkCommandHandler(IDbContextFactory<LinkModuleDatabaseContext> contextFactory)
    : IRequestHandler<CreateLinkCommand, CreateLinkCommandResult>
{
    public async Task<CreateLinkCommandResult> Handle(CreateLinkCommand request, CancellationToken cancellationToken)
    {
        var context = await contextFactory.CreateDbContextAsync(cancellationToken);

        var uniqueKey = await GetUniqueKeyAsync(context, cancellationToken);

        var link = new Link
        {
            Id = Guid.NewGuid(),
            Name = request.Parameter.Name,
            Url = request.Parameter.Url,
            UniqueKey = uniqueKey
        };

        await context.Links.AddAsync(link, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return new CreateLinkCommandResult()
        {
            LinkId = link.Id
        };
    }

    private async Task<string> GetUniqueKeyAsync(LinkModuleDatabaseContext context, CancellationToken cancellationToken)
    {
        var uniqueKey = RandomString(8);
        var isExist = await context.Links.AnyAsync(x => x.UniqueKey == uniqueKey, cancellationToken: cancellationToken);
        return isExist ? await GetUniqueKeyAsync(context, cancellationToken) : uniqueKey;
    }

    private static readonly Random Random = new();

    private static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }
}