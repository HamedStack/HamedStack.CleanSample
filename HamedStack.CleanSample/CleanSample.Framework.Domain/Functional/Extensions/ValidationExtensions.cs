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

    public static Maybe<TValue> ToMaybe<TValue, TError>(this Validation<TValue, TError> validation)
    {
        return validation.Match(
            some => Maybe<TValue>.Just(some!),
            _ => Maybe<TValue>.Nothing()
        );
    }
}