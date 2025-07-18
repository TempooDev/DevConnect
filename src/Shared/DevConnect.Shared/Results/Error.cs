namespace DevConnect.Shared.Result;

public record Error(string Code, string Message)
{
    public static readonly Error None = new("None", string.Empty);
    public static readonly Error NullValue = new("Error.Null", "Value is null");
}
