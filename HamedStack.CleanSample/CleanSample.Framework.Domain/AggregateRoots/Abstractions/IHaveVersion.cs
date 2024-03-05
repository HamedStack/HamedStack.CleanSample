// ReSharper disable CommentTypo
namespace CleanSample.Framework.Domain.AggregateRoots.Abstractions;

public interface IHaveRowVersion
{
    byte[] RowVersion { get; set; }
}