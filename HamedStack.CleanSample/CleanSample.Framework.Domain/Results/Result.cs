using System.Text.Json.Serialization;

namespace CleanSample.Framework.Domain.Results;

public class Result
{
    protected Result() { }

    [JsonInclude]
    public ResultStatus Status { get; protected set; } = ResultStatus.Success;
    public bool IsSuccess { get; protected set; } = true;
    public string[] ErrorMessages { get; protected set; } = new string[] { };
    public string SuccessMessage { get; protected set; } = string.Empty;
    [JsonInclude]
    public IDictionary<string, object> Metadata { get; protected set; } = new Dictionary<string, object>();

    public static Result Success()
    {
        return new Result { IsSuccess = true, Status = ResultStatus.Success };
    }

    public static Result Success(string successMessage)
    {
        return new Result { IsSuccess = true, Status = ResultStatus.Success, SuccessMessage = successMessage };
    }

    public static Result Error(params string[] errorMessages)
    {
        return new Result { IsSuccess = false, ErrorMessages = errorMessages, Status = ResultStatus.Error };
    }

    public static Result Forbidden(params string[] errorMessages)
    {
        return new Result { IsSuccess = false, ErrorMessages = errorMessages, Status = ResultStatus.Forbidden };
    }

    public static Result Unauthorized(params string[] errorMessages)
    {
        return new Result { IsSuccess = false, ErrorMessages = errorMessages, Status = ResultStatus.Unauthorized };
    }

    public static Result Invalid(params string[] errorMessages)
    {
        return new Result { IsSuccess = false, ErrorMessages = errorMessages, Status = ResultStatus.Invalid };
    }

    public static Result NotFound(params string[] errorMessages)
    {
        return new Result { IsSuccess = false, ErrorMessages = errorMessages, Status = ResultStatus.NotFound };
    }

    public static Result Conflict(params string[] errorMessages)
    {
        return new Result { IsSuccess = false, ErrorMessages = errorMessages, Status = ResultStatus.Conflict };
    }

    public static Result Unavailable(params string[] errorMessages)
    {
        return new Result { IsSuccess = false, ErrorMessages = errorMessages, Status = ResultStatus.Unavailable };
    }

    public static Result Unsupported(params string[] errorMessages)
    {
        return new Result { IsSuccess = false, ErrorMessages = errorMessages, Status = ResultStatus.Unsupported };
    }

    public void AddMetadata(string key, object value)
    {
        Metadata.Add(key, value);
    }
}