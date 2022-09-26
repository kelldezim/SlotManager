using SlotManager.Core.Abstractions;
using SlotManager.Core.Entities;
using SlotManager.Core.Exceptions;
using SlotManager.Core.Policies;
using SlotManager.Core.ValueObjects;

namespace SlotManager.Core.DomainServices
{
    public sealed class ParkingReservationService : IParkingReservationService
    {
        private readonly IEnumerable<IReservationPolicy> _reservationPolicies;
        private readonly IClock _clock;

        public ParkingReservationService(IEnumerable<IReservationPolicy> reservationPolicies, IClock clock)
        {
            _reservationPolicies = reservationPolicies;
            _clock = clock;
        }

        public void ReserveSpotForVehicle(IEnumerable<WeeklyParkingSpot> allParkingSpots, 
                                          JobTitle jobTitle, 
                                          WeeklyParkingSpot weeklyParkingSpotToReserve, 
                                          Reservation reservation)
        {
            var parkingSpotId = weeklyParkingSpotToReserve.Id;
            var policy = _reservationPolicies.SingleOrDefault(x => x.CanBeApplied(jobTitle));

            if(policy is null)
            {
                throw new NoReservationPolicyFoundException(jobTitle);
            }

            if(policy.CanReserve(allParkingSpots, reservation.EmployeeName) is false)
            {
                throw new CannotReserveParkingSpotException(parkingSpotId);
            }

            weeklyParkingSpotToReserve.AddReservation(reservation, new Date(_clock.Current()));
        }
    }
}
