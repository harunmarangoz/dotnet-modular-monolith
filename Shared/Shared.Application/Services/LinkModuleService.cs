namespace Shared.Application.Services;

public interface ILinkModuleService
{
    Task<string> GetRedirectUrlFromUniqueKeyAsync(string uniqueKey);
}