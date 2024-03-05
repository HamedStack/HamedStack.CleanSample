namespace CleanSample.Framework.Application.Paging;

/// <summary>
/// Represents a paged list of items that is created from a <see cref="IQueryable{T}"/> source.
/// </summary>
/// <typeparam name="T">The type of elements in the list.</typeparam>
public class PagedList<T> : IPagedList<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PagedList{T}"/> class.
    /// </summary>
    /// <param name="source">
    /// The <see cref="IQueryable{T}"/> source from which the paged list is created.
    /// </param>
    /// <param name="pageNumber">The current page number (1-based index).</param>
    /// <param name="pageSize">The number of items to include on each page.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if the <paramref name="source"/> is null.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the <paramref name="pageNumber"/> or <paramref name="pageSize"/> is less than or
    /// equal to 0.
    /// </exception>
    /// <remarks>
    /// This constructor initializes a paged list from an <see cref="IQueryable{T}"/> source. It
    /// calculates various properties of the paged list, such as the total number of items, the
    /// number of pages, and the items on the current page.
    /// </remarks>
    public PagedList(IQueryable<T> source, int pageNumber, int pageSize)
    {
        Initialize(source, pageNumber, pageSize);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PagedList{T}"/> class from an <see
    /// cref="IEnumerable{T}"/> source.
    /// </summary>
    /// <param name="source">
    /// The <see cref="IEnumerable{T}"/> source from which the paged list is created.
    /// </param>
    /// <param name="pageNumber">The current page number (1-based index).</param>
    /// <param name="pageSize">The number of items to include on each page.</param>
    /// <remarks>
    /// This constructor initializes a paged list from an <see cref="IEnumerable{T}"/> source by
    /// converting it to an <see cref="IQueryable{T}"/> source. It calculates various properties of
    /// the paged list, such as the total number of items, the number of pages, and the items on the
    /// current page.
    /// </remarks>
    public PagedList(IEnumerable<T> source, int pageNumber, int pageSize)
    {
        Initialize(source.AsQueryable(), pageNumber, pageSize);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PagedList{T}"/> class asynchronously from an
    /// <see cref="IAsyncEnumerable{T}"/> source.
    /// </summary>
    /// <param name="source">
    /// The <see cref="IAsyncEnumerable{T}"/> source from which the paged list is created.
    /// </param>
    /// <param name="pageNumber">The current page number (1-based index).</param>
    /// <param name="pageSize">The number of items to include on each page.</param>
    /// <remarks>
    /// This constructor initializes a paged list asynchronously from an <see
    /// cref="IAsyncEnumerable{T}"/> source. It calculates various properties of the paged list,
    /// such as the total number of items, the number of pages, and the items on the current page.
    /// </remarks>
    public PagedList(IAsyncEnumerable<T> source, int pageNumber, int pageSize)
    {
        InitializeAsync(source, pageNumber, pageSize).GetAwaiter().GetResult();
    }

    /// <inheritdoc/>
    public int FirstItemOnPage => (PageNumber - 1) * PageSize + 1;

    /// <inheritdoc/>
    public bool HasNextPage => PageNumber < PageCount;

    /// <inheritdoc/>
    public bool HasPreviousPage => PageNumber > 1;

    /// <inheritdoc/>
    public bool IsFirstPage => PageNumber == 1;

    /// <inheritdoc/>
    public bool IsLastPage => PageNumber == PageCount;

    /// <inheritdoc/>
    public IList<T> Items { get; private set; } = new List<T>();

    /// <inheritdoc/>
    public int LastItemOnPage => FirstItemOnPage + Items.Count - 1;

    /// <inheritdoc/>
    public int PageCount { get; private set; }

    /// <inheritdoc/>
    public int PageNumber { get; private set; }

    /// <inheritdoc/>
    public int PageSize { get; private set; }

    /// <inheritdoc/>
    public int TotalCount { get; private set; }

    /// <inheritdoc/>
    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= Items.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
            }
            return Items[index];
        }
    }

    private void Initialize(IQueryable<T> source, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = source.Count();
        PageCount = (int)Math.Ceiling(TotalCount / (double)PageSize);
        Items = source.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
    }

    private async Task InitializeAsync(IAsyncEnumerable<T> source, int pageNumber, int pageSize)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));

        PageNumber = pageNumber;
        PageSize = pageSize;
        // ReSharper disable once PossibleMultipleEnumeration
        TotalCount = await source.CountAsync();
        PageCount = (int)Math.Ceiling(TotalCount / (double)PageSize);
        // ReSharper disable once PossibleMultipleEnumeration
        Items = await source.SkipAsync((PageNumber - 1) * PageSize).TakeAsync(PageSize).ToListAsync();
    }
}