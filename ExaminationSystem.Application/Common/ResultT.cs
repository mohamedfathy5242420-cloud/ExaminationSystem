namespace ExaminationSystem.Application.Common;

public sealed class Result<TValue> : Result
{
    private Result(TValue? value, bool isSuccess, string? error)
        : base(isSuccess, error)
    {
        Value = value;
    }

    public TValue? Value { get; }

    public static Result<TValue> Success(TValue value) => new(value, true, null);

    public static new Result<TValue> Failure(string error) => new(default, false, error);
}
