using PrimativeExtensions;

namespace ExtensionHelpersForTests;

public static class ValidationExtensionHelpers
{
    public static void AssertSuccess<T>(this Validation<T> validation, Action<T> func)
    {
        validation.Match(func, (error) => Assert.Fail($"Validation failed {error}") );
    }
}