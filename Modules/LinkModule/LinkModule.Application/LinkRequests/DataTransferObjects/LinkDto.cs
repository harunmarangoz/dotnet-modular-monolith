namespace LinkModule.Application.LinkRequests.DataTransferObjects;

public class LinkDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string UniqueKey { get; set; }
}