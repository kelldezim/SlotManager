using SlotManager.Core.ValueObjects;

namespace SlotManager.Core.Exceptions
{
    public sealed class NoReservationPolicyFoundException : CustomException
    {
        public NoReservationPolicyFoundException(JobTitle jobTitle) : base($"No reservation policy has been found for {jobTitle} job title.")
        {
            JobTitle = jobTitle;
        }

        public JobTitle JobTitle { get; }
    }
}
