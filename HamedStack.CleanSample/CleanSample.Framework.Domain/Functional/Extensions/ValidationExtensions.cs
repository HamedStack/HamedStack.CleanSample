using CleanSample.Framework.Domain.Results;

namespace CleanSample.Framework.Domain.Functional.Extensions;

public static class ValidationExtensions
{
    public static Validation<TValue, TError> CombineWith<TValue, TError>(
        this Validation<TValue, TError> source, Validation<TValue, TError> other)
    {
        if (source.IsValid && other.IsValid) return Validation<TValue, TError>.Valid(source.Value!);

        var combinedErrors = source.GetErrors().Concat(other.GetErrors()).ToArray();

        return Validation<TValue, TError>.Invalid(combinedErrors);
    }

    public static Either<TValue, IEnumerable<TError>> ToEither<TValue, TError>(
            this Validation<TValue, TError> validation)
    {
        return validation.Match(
            some => Either<TValue, IEnumerable<TError>>.CreateLeft(some!),
            Either<TValue, IEnumerable<TError>>.CreateRight
        );
    }

    /*
    public static Result<TValue> ToGenericResult<TValue, TError>(this Validation<TValue, TError> validation)
    {
        return validation.Match(
            Result<TValue>.Success,
            errors =>
            {
                var err = errors
                    .Select((item, index) => new { Key = index, Value = item })
                    .ToDictionary(pair => pair.Key.ToString(), pair => (object?)pair.Value);
                return Result<TValue>.Failure(metaData: err);
            }
        );
    }
    */

    public static Option<TValue> ToOption<TValue, TError>(this Validation<TValue, TError> validation)
    {
        return validation.Match(
            some => Option<TValue>.Some(some!),
            _ => Option<TValue>.None()
        );
    }
}