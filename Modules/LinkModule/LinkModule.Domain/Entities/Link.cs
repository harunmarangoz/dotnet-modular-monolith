using Shared.Domain.Entities;

namespace LinkModule.Domain.Entities;

public class Link : BaseEntity
{
    public string Name { get; set; }
    public string Url { get; set; }
    public string UniqueKey { get; set; }
}