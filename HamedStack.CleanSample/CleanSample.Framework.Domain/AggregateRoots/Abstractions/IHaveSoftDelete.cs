namespace CleanSample.Framework.Domain.AggregateRoots.Abstractions;

public interface IHaveSoftDelete : IHaveDeletionAudit
{
    bool IsDeleted { get; set; }
}

public interface IHaveDeletionAudit
{
    string? DeletedBy { get; set; }

    DateTimeOffset DeletedOn { get; set; }
}