using SlotManager.Api.Exceptions;

namespace SlotManager.Api.ValueObjects
{
    public sealed record EmployeeName(string Value)
    {
        public string Value { get; } = Value ?? throw new InvalidEmployeeName();

        public static implicit operator string(EmployeeName name) => name.Value;

        public static implicit operator EmployeeName(string name) => new EmployeeName(name);
    }
}
