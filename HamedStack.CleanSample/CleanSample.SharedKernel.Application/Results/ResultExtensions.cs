namespace CleanSample.SharedKernel.Application.Results;

public static class ResultExtensions
{
    public static Task<IResult> ToTask(this IResult result)
    {
        return Task.FromResult(result);
    }
    public static ValueTask<IResult> ToValueTask(this IResult result)
    {
        return ValueTask.FromResult(result);
    }

    public static Task<IResult<TResult>> ToTask<TResult>(this IResult<TResult> result)
    {
        return Task.FromResult(result);
    }
    public static ValueTask<IResult<TResult>> ToValueTask<TResult>(this IResult<TResult> result)
    {
        return ValueTask.FromResult(result);
    }

    public static IResult<T> ToResult<T>(this T result, ResultStatus resultStatus = ResultStatus.Success)
    {
        return new Result<T>(resultStatus, result);
    }
    public static IResult<T> ToResult<T>(this T result, ResultStatus resultStatus, string error)
    {
        return new Result<T>(resultStatus, result, error);
    }
    public static IResult<T> ToResult<T>(this T result, ResultStatus resultStatus, Error error)
    {
        return new Result<T>(resultStatus, result, error);
    }
    public static IResult ToResult(this object result, ResultStatus resultStatus)
    {
        return new Result(resultStatus, result);
    }

    public static IResult ToResult(this object result, ResultStatus resultStatus, string error)
    {
        return new Result(resultStatus, result, error);
    }
    public static IResult ToResult(this object result, ResultStatus resultStatus, Error error)
    {
        return new Result(resultStatus, result, error);
    }

    public static Task<IResult<T>> ToResult<T>(this Task<T> result, ResultStatus resultStatus = ResultStatus.Success)
    {
        return new Result<T>(resultStatus, result.Result).ToTask();
    }

    public static Task<IResult<T>> ToResult<T>(this Task<T> result, ResultStatus resultStatus, string error)
    {
        return new Result<T>(resultStatus, result.Result, error).ToTask();
    }
    public static Task<IResult<T>> ToResult<T>(this Task<T> result, ResultStatus resultStatus, Error error)
    {
        return new Result<T>(resultStatus, result.Result, error).ToTask();
    }

    public static Task<IResult> ToResult(this Task<object> result, ResultStatus resultStatus)
    {
        return new Result(resultStatus, result).ToTask();
    }

    public static Task<IResult> ToResult(this Task<object> result, ResultStatus resultStatus, string error)
    {
        return new Result(resultStatus, result.Result, error).ToTask();
    }

    public static Task<IResult> ToResult(this Task<object> result, ResultStatus resultStatus, Error error)
    {
        return new Result(resultStatus, result.Result, error).ToTask();
    }
    public static ValueTask<IResult<T>> ToResult<T>(this ValueTask<T> result, ResultStatus resultStatus = ResultStatus.Success)
    {
        return new Result<T>(resultStatus, result.Result).ToValueTask();
    }
    public static ValueTask<IResult<T>> ToResult<T>(this ValueTask<T> result, ResultStatus resultStatus, string error)
    {
        return new Result<T>(resultStatus, result.Result, error).ToValueTask();
    }
    public static ValueTask<IResult<T>> ToResult<T>(this ValueTask<T> result, ResultStatus resultStatus, Error error)
    {
        return new Result<T>(resultStatus, result.Result, error).ToValueTask();
    }
    public static ValueTask<IResult> ToResult(this ValueTask<object> result, ResultStatus resultStatus = ResultStatus.Success)
    {
        return new Result(resultStatus, result).ToValueTask();
    }
    public static ValueTask<IResult> ToResult(this ValueTask<object> result, ResultStatus resultStatus, string error)
    {
        return new Result(resultStatus, result, error).ToValueTask();
    }

    public static ValueTask<IResult> ToResult(this ValueTask<object> result, ResultStatus resultStatus, Error error)
    {
        return new Result(resultStatus, result, error).ToValueTask();
    }
}