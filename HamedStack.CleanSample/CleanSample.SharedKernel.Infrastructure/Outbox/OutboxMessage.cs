namespace CleanSample.SharedKernel.Infrastructure.Outbox;

internal class OutboxMessage
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset? ProcessedOn { get; set; }
    public bool IsProcessed { get; set; }
}