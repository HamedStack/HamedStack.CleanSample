// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMemberInSuper.Global

namespace CleanSample.Domain.Paging;

/// <summary>
/// Represents a pageable list of items.
/// </summary>
/// <typeparam name="T">The type of elements in the list.</typeparam>
public interface IPagedList<T>
{
    /// <summary>
    /// Gets the number of the first item on the current page.
    /// </summary>
    int FirstItemOnPage { get; }

    /// <summary>
    /// Indicates whether the current page has a subsequent page.
    /// </summary>
    bool HasNextPage { get; }

    /// <summary>
    /// Indicates whether the current page has a preceding page.
    /// </summary>
    bool HasPreviousPage { get; }

    /// <summary>
    /// Indicates whether the current page is the first page.
    /// </summary>
    bool IsFirstPage { get; }

    /// <summary>
    /// Indicates whether the current page is the last page.
    /// </summary>
    bool IsLastPage { get; }

    /// <summary>
    /// Gets the items on the current page.
    /// </summary>
    IList<T> Items { get; }

    /// <summary>
    /// Gets the number of the last item on the current page.
    /// </summary>
    int LastItemOnPage { get; }

    /// <summary>
    /// Gets the total number of pages.
    /// </summary>
    int PageCount { get; }

    /// <summary>
    /// Gets the current page number.
    /// </summary>
    int PageNumber { get; }

    /// <summary>
    /// Gets the number of items per page.
    /// </summary>
    int PageSize { get; }

    /// <summary>
    /// Gets the total count of items across all pages.
    /// </summary>
    int TotalCount { get; }

    /// <summary>
    /// Gets the item at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the item to get.</param>
    T this[int index] { get; }
}