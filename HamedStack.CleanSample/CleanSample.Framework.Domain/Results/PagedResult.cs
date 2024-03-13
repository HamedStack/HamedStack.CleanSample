using System.Text.Json.Serialization;

namespace CleanSample.Framework.Domain.Results;

public class PagedResult<T> : Result<T>
{
    protected PagedResult()
    {
    }

    [JsonInclude] public PagedInfo? PagedInfo { get; set; }
    public bool HasPagedInfo => PagedInfo is not null;
    public static PagedResult<T> Success(T? value, PagedInfo pagedInfo)
    {
        return new PagedResult<T>
        {
            IsSuccess = true,
            Status = ResultStatus.Success,
            Value = value,
            PagedInfo = new PagedInfo
            {
                FirstItemOnPage = pagedInfo.FirstItemOnPage,
                HasNextPage = pagedInfo.HasNextPage,
                HasPreviousPage = pagedInfo.HasPreviousPage,
                IsFirstPage = pagedInfo.IsFirstPage,
                IsLastPage = pagedInfo.IsLastPage,
                LastItemOnPage = pagedInfo.LastItemOnPage,
                PageCount = pagedInfo.PageCount,
                PageNumber = pagedInfo.PageNumber,
                PageSize = pagedInfo.PageSize,
                TotalCount = pagedInfo.TotalCount
            }
        };
    }

    public static PagedResult<T> Success(T? value, PagedInfo pagedInfo, string successMessage)
    {
        return new PagedResult<T>
        {
            IsSuccess = true,
            Status = ResultStatus.Success,
            SuccessMessage = successMessage,
            Value = value,
            PagedInfo = new PagedInfo
            {
                FirstItemOnPage = pagedInfo.FirstItemOnPage,
                HasNextPage = pagedInfo.HasNextPage,
                HasPreviousPage = pagedInfo.HasPreviousPage,
                IsFirstPage = pagedInfo.IsFirstPage,
                IsLastPage = pagedInfo.IsLastPage,
                LastItemOnPage = pagedInfo.LastItemOnPage,
                PageCount = pagedInfo.PageCount,
                PageNumber = pagedInfo.PageNumber,
                PageSize = pagedInfo.PageSize,
                TotalCount = pagedInfo.TotalCount
            }
        };
    }

    public new static PagedResult<T> Error(T? value, params string[] errorMessages)
    {
        return new PagedResult<T>
        {
            IsSuccess = false,
            Status = ResultStatus.Error,
            Value = value,
            PagedInfo = null,
            ErrorMessages = errorMessages
        };
    }

    public new static PagedResult<T> Forbidden(T? value, params string[] errorMessages)
    {
        return new PagedResult<T>
        {
            IsSuccess = false,
            Status = ResultStatus.Forbidden,
            Value = value,
            PagedInfo = null,
            ErrorMessages = errorMessages
        };
    }

    public new static PagedResult<T> Unauthorized(T? value, params string[] errorMessages)
    {
        return new PagedResult<T>
        {
            IsSuccess = false,
            Status = ResultStatus.Unauthorized,
            Value = value,
            PagedInfo = null,
            ErrorMessages = errorMessages
        };
    }

    public new static PagedResult<T> Invalid(T? value, params string[] errorMessages)
    {
        return new PagedResult<T>
        {
            IsSuccess = false,
            Status = ResultStatus.Invalid,
            Value = value,
            PagedInfo = null,
            ErrorMessages = errorMessages
        };
    }

    public new static PagedResult<T> NotFound(T? value, params string[] errorMessages)
    {
        return new PagedResult<T>
        {
            IsSuccess = false,
            Status = ResultStatus.NotFound,
            Value = value,
            PagedInfo = null,
            ErrorMessages = errorMessages
        };
    }

    public new static PagedResult<T> Conflict(T? value, params string[] errorMessages)
    {
        return new PagedResult<T>
        {
            IsSuccess = false,
            Status = ResultStatus.Conflict,
            Value = value,
            PagedInfo = null,
            ErrorMessages = errorMessages
        };
    }

    public new static PagedResult<T> Unavailable(T? value, params string[] errorMessages)
    {
        return new PagedResult<T>
        {
            IsSuccess = false,
            Status = ResultStatus.Unavailable,
            Value = value,
            PagedInfo = null,
            ErrorMessages = errorMessages
        };
    }

    public new static PagedResult<T> Unsupported(T? value, params string[] errorMessages)
    {
        return new PagedResult<T>
        {
            IsSuccess = false,
            Status = ResultStatus.Unsupported,
            Value = value,
            PagedInfo = null,
            ErrorMessages = errorMessages
        };
    }
}