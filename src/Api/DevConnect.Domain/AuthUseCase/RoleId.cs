namespace DevConnect.Domain.AuthUseCase;

public sealed record RoleId(int Value)
{
    public static implicit operator int(RoleId roleId) => roleId.Value;
    public static implicit operator RoleId(int value) => new(value);

    public override string ToString() => Value.ToString();
}
