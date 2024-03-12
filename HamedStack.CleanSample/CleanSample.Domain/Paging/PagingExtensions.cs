// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedVariable

namespace CleanSample.Domain.Paging;

/// <summary>
/// Provides extension methods for pagination on various data structures.
/// </summary>
public static class PagingExtensions
{
    /// <summary>
    /// Converts the source IQueryable to a paged IEnumerable.
    /// </summary>
    /// <typeparam name="TEntity">The type of elements in the source.</typeparam>
    /// <param name="query">The query to paginate.</param>
    /// <param name="pageIndex">The page index to retrieve.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A paged IEnumerable.</returns>
    public static IEnumerable<TEntity> ToPaged<TEntity>(this IQueryable<TEntity> query, int pageIndex, int pageSize)
    {
        return query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
    }

    /// <summary>
    /// Converts the source IEnumerable to a paged list.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source.</typeparam>
    /// <param name="query">The source data.</param>
    /// <param name="pageIndex">The page index to retrieve.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A paged list of items.</returns>
    public static IEnumerable<T> ToPaged<T>(this IEnumerable<T> query, int pageIndex, int pageSize)
    {
        return query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
    }

    /// <summary>
    /// Converts the source array to a paged list.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source.</typeparam>
    /// <param name="query">The source array.</param>
    /// <param name="pageIndex">The page index to retrieve.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A paged list of items.</returns>
    public static IEnumerable<T> ToPaged<T>(this T[] query, int pageIndex, int pageSize)
    {
        return query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
    }

    /// <summary>
    /// Converts the source IAsyncEnumerable to a paged list asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source.</typeparam>
    /// <param name="source">The source asynchronous enumerable.</param>
    /// <param name="pageIndex">The page index to retrieve.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A paged list of items.</returns>
    public static async IAsyncEnumerable<T> ToPagedAsync<T>(this IAsyncEnumerable<T> source, int pageIndex, int pageSize)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        var itemsToSkip = (pageIndex - 1) * pageSize;
        var skipped = 0;
        var taken = 0;

        await foreach (var item in source)
        {
            if (skipped < itemsToSkip)
            {
                skipped++;
                continue;
            }

            if (taken < pageSize)
            {
                yield return item;
                taken++;
            }
            else
            {
                yield break;
            }
        }
    }

    /// <summary>
    /// Converts the source IQueryable to a paged list.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source.</typeparam>
    /// <param name="source">The source query.</param>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A paged list.</returns>
    public static IPagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageNumber, int pageSize)
    {
        return new PagedList<T>(source, pageNumber, pageSize);
    }

    /// <summary>
    /// Converts the source IEnumerable to a paged list.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source.</typeparam>
    /// <param name="source">The source enumerable.</param>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A paged list.</returns>
    public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageNumber, int pageSize)
    {
        return new PagedList<T>(source.AsQueryable(), pageNumber, pageSize);
    }

    /// <summary>
    /// Converts the source array to a paged list.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source.</typeparam>
    /// <param name="source">The source array.</param>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A paged list.</returns>
    public static IPagedList<T> ToPagedList<T>(this T[] source, int pageNumber, int pageSize)
    {
        return new PagedList<T>(source.AsQueryable(), pageNumber, pageSize);
    }

    /// <summary>
    /// Converts the source asynchronous enumerable to an asynchronous paged list.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source.</typeparam>
    /// <param name="source">The asynchronous source.</param>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>An asynchronous paged list.</returns>
    public static Task<IPagedList<T>> ToPagedListAsync<T>(this IAsyncEnumerable<T> source, int pageNumber, int pageSize)
    {
        return Task.FromResult(new PagedList<T>(source, pageNumber, pageSize) as IPagedList<T>);
    }

    /// <summary>
    /// Internal method to count the number of items in an asynchronous enumerable.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source.</typeparam>
    /// <param name="source">The asynchronous source.</param>
    /// <returns>The count of items in the source.</returns>
    internal static async Task<int> CountAsync<T>(this IAsyncEnumerable<T> source)
    {
        var count = 0;
        await foreach (var item in source)
        {
            count += 1;
        }
        return count;
    }

    /// <summary>
    /// Internal method to skip items in an asynchronous enumerable.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source.</typeparam>
    /// <param name="source">The asynchronous source.</param>
    /// <param name="count">The number of items to skip.</param>
    /// <returns>An asynchronous enumerable with items skipped.</returns>
    internal static async IAsyncEnumerable<T> SkipAsync<T>(this IAsyncEnumerable<T> source, int count)
    {
        var skipped = 0;
        await foreach (var item in source)
        {
            if (skipped < count)
            {
                skipped += 1;
                continue;
            }
            yield return item;
        }
    }

    /// <summary>
    /// Internal method to take items from an asynchronous enumerable.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source.</typeparam>
    /// <param name="source">The asynchronous source.</param>
    /// <param name="count">The number of items to take.</param>
    /// <returns>An asynchronous enumerable with specified number of items.</returns>
    internal static async IAsyncEnumerable<T> TakeAsync<T>(this IAsyncEnumerable<T> source, int count)
    {
        var taken = 0;
        await foreach (var item in source)
        {
            if (taken >= count)
            {
                break;
            }
            yield return item;
            taken += 1;
        }
    }

    /// <summary>
    /// Internal method to convert an asynchronous enumerable to a list.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source.</typeparam>
    /// <param name="source">The asynchronous source.</param>
    /// <returns>A list containing all items from the source.</returns>
    internal static async Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> source)
    {
        var list = new List<T>();
        await foreach (var item in source)
        {
            list.Add(item);
        }
        return list;
    }
}