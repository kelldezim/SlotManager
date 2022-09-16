using SlotManager.Api.Exceptions;

namespace SlotManager.Api.ValueObjects
{
    public sealed record ParkingSpotName(string Value)
    {
        public string Value { get; } = Value ?? throw new InvalidParkingSpotNameException();

        public static implicit operator string(ParkingSpotName name) => name.Value;

        public static implicit operator ParkingSpotName(string name) => new ParkingSpotName(name);
    }
}
