namespace CleanSample.Framework.Domain.AggregateRoots.Abstractions;

public interface IHaveAudit : IHaveCreationAudit, IHaveModificationAudit
{
}

public interface IHaveCreationAudit
{
    string? CreatedBy { get; set; }

    DateTimeOffset CreatedOn { get; set; }
}

public interface IHaveModificationAudit
{
    string? ModifiedBy { get; set; }
    DateTimeOffset? ModifiedOn { get; set; }
}