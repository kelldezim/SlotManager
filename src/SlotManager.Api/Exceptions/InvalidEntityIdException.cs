namespace SlotManager.Api.Exceptions
{
    public sealed class InvalidEntityIdException : CustomerException
    {
        public object Id { get; }

        public InvalidEntityIdException(object id) : base($"Cannot set: {id}  as entity identifier.")
            => Id = id;
    }
}
