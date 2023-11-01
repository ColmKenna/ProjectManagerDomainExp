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
}