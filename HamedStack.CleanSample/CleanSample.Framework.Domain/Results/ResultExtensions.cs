using CleanSample.Framework.Domain.Paging;

namespace CleanSample.Framework.Domain.Results;

public static class ResultExtensions
{
    public static PagedResult<T> ToPagedResult<T>(this Result<T> result, PagedInfo pagedInfo)
    {
        return PagedResult<T>.Success(result.Value, pagedInfo, result.SuccessMessage);
    }

    public static PagedResult<IList<T>> ToPagedResult<T>(this PagedList<T> result, string successMessage = "")
    {
        return PagedResult<IList<T>>.Success(result.Items, new PagedInfo()
        {
            FirstItemOnPage = result.FirstItemOnPage,
            HasNextPage = result.HasNextPage,
            HasPreviousPage = result.HasPreviousPage,
            IsFirstPage = result.IsFirstPage,
            IsLastPage = result.IsLastPage,
            LastItemOnPage = result.LastItemOnPage,
            PageCount = result.PageCount,
            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount,
        }, successMessage);
    }
}