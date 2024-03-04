// ReSharper disable CommentTypo
namespace CleanSample.SharedKernel.Domain.AggregateRoots.Abstractions;

public interface IHaveRowVersion
{
    byte[] RowVersion { get; set; }
}