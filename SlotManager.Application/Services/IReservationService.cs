using SlotManager.Application.Commands;
using SlotManager.Application.DTO;

namespace SlotManager.Application.Services
{
    public interface IReservationService
    {
        Task<Guid?> ReserveForVehicleAsync(ReserveParkingSpotForVehicle command);
        Task ReserveForCleaningAsync(ReserveParkingSpotForCleaning command);
        Task<bool> DeleteAsync(DeleteReservation command);
        Task<ReservationDto> GetAsync(Guid id);
        Task<IEnumerable<ReservationDto>> GetAllWeeklyAsync();
        Task<bool> ChangeReservationLicencePlateAsync(ChangeReservationLicensePlate command);
    }
}