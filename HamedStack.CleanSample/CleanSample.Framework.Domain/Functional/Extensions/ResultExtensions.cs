﻿// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

using CleanSample.Framework.Domain.Results;

#pragma warning disable CS1591

namespace CleanSample.Framework.Domain.Functional.Extensions;


public static class ResultExtensions
{
    public static Result Finally(this Result result, Action<Result> action)
    {
        action(result);
        return result;
    }

    public static Result Flatten(this Result<Result> result)
    {
        return result.IsSuccess
            ? Result.Success()
            : Result.Error(result.ErrorMessages);
    }

    public static Result IfFailure(this Result result, Action<Result> action)
    {
        if (!result.IsSuccess) action(result);
        return result;
    }

    public static Result IfSuccess(this Result result, Action<Result> action)
    {
        if (result.IsSuccess) action(result);
        return result;
    }

    public static Result IfSuccess(this Result result, Action action)
    {
        if (result.IsSuccess) action();
        return result;
    }
    public static Result IfFailure(this Result result, Action action)
    {
        if (!result.IsSuccess) action();
        return result;
    }
    public static Result Join(this IEnumerable<Result> results, string separator = ", ")
    {
        var failures = results.Where(r => !r.IsSuccess).ToList();
        if (!failures.Any()) return Result.Success();

        var combinedMessage = string.Join(separator, failures.SelectMany(f => f.ErrorMessages));
        return Result.Error(combinedMessage);
    }
}