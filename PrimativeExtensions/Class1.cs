namespace PrimativeExtensions;

public static class Validation
{
    public static TR OnValid<T, U, TR>( Validation<T> validation, Validation<U> validation2, Func<T, U, TR> onValid, Func<Validation<T>, TR> onInvalid, Func<Validation<U>, TR> onInvalid2)
    {
        if (validation.IsFailure)
        {
            return onInvalid(validation);
        }

        if (validation2.IsFailure)
        {
            return onInvalid2(validation2);
        }

        return onValid(validation.Value, validation2.Value);
    }
    
    public static void OnValid<T, U>( Validation<T> validation, Validation<U> validation2, Action<T, U> onValid, Action<Validation<T>> onInvalid, Action<Validation<U>> onInvalid2)
    {
        if (validation.IsFailure)
        {
            onInvalid(validation);
            return;
        }

        if (validation2.IsFailure)
        {
             onInvalid2(validation2);
             return;
        }

        onValid(validation.Value, validation2.Value);
    }

}
public static class ValidationExtensions
{
    public static IEnumerable<T> AsEnumerable<T>(this Validation<IEnumerable<T>> validation)
    {
        if (validation.IsFailure)
        {
            yield break;
        }

        foreach (var item in validation.Value)
        {
            yield return item;
        }
    }
    
    public static TR OnSuccess<T,U, TR>(this Validation<T> validation, Func<T, TR> func)
    {
        if (validation.IsFailure)
        {
            throw new InvalidCastException(validation.ErrorMessage);
        }

        return func(validation.Value);
    }
    
    public static void Match<T,U> (this Validation<T> validation, Validation<U> otherValidation, Action<T,U> success, Action<Validation<T>> failure, Action<Validation<U>> failure2)
    {
        if (validation.IsFailure)
        {
            failure(validation);
            return;
        }

        if (otherValidation.IsFailure)
        {
            failure2(otherValidation);
            return;
        }

        success(validation.Value, otherValidation.Value);
    }
    
    public static TR  Match<T,U,TR> (this Validation<T> validation, Validation<U> otherValidation, Func<T,U,TR> success, Func<Validation<T>,TR> failure, Func<Validation<U>,TR> failure2)
    {
        if (validation.IsFailure)
        {
            return failure(validation);
        }

        if (otherValidation.IsFailure)
        {
            return failure2(otherValidation);
        }

        return success(validation.Value, otherValidation.Value);
    }
}