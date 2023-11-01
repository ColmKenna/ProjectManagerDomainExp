namespace PrimativeExtensions;

public class Validation<T>
{
    private readonly T value;
    public bool IsValid { get; }
    public string ErrorMessage { get; }

    //Throw an exception if IsValid is false

    public T Value
    {
        get
        {
            if (IsFailure)
            {
                throw new InvalidCastException(ErrorMessage);
            }
            return value;
        }
    }

    public bool IsFailure => !IsValid;

    private Validation(T value, bool isValid, string errorMessage)
    {
        this.value = value;
        IsValid = isValid;
        ErrorMessage = errorMessage;
    }

    public static Validation<T> Success(T value) => new Validation<T>(value, true, string.Empty);

    public static Validation<T> Fail(string errorMessage) => new Validation<T>(default, false, errorMessage);
    public static implicit operator Validation<T>(T value) => Success(value);

    public static implicit operator T(Validation<T> value)
    {
        if (value.IsFailure)
        {
            throw new InvalidCastException(value.ErrorMessage);
        }

        return value.Value;
    }

    public Validation<TR> OnSuccess<TR>(Func<T, TR> func)
    {
        if (IsFailure)
        {
            return Validation<TR>.Fail(ErrorMessage);
        }

        return Validation<TR>.Success(func(this.Value));
    }

    public Validation<TR> OnSuccess<TR>(Func<T, Validation<TR>> func)
    {
        if (IsFailure)
        {
            return Validation<TR>.Fail(ErrorMessage);
        }

        return func(this.Value);
    }
    
    public void ActionOnSuccess(Action<T> func)
    {
        if (IsFailure)
        {
            return;
        }

        func(this.Value);
    }
    public IEnumerable<TR> MapAsEnumerable<TR>(Func<T, IEnumerable<TR>> func){
        if (IsFailure)
        {
            return Enumerable.Empty<TR>();
        }

        return func(this.Value);
    }

    

    
    public R Match<R>(Func<T, R> success, Func<string, R> failure)
    {
        return IsFailure ? failure(ErrorMessage) : success(Value);
    }

    public void Match(Action<T> success, Action<string> failure)
    {
        if (IsFailure)
        {
            failure(ErrorMessage);
        }
        else
        {
            success(Value);
        }
    }

    public static Validation<R> Bind<T, R>(Validation<T> validation, Func<T, Validation<R>> func)
    {
        if (validation.IsFailure)
        {
            return Validation<R>.Fail(validation.ErrorMessage);
        }

        return func(validation.Value);
    }
    
    public Maybe<T> ToMaybe()
    {
        return IsFailure ? Maybe<T>.None : Value;
    }

    public IEnumerable<T> AsEnumerable()
    {
        if (IsFailure)
        {
            yield break;
        }

        yield return Value;

    }
    
    
}