using SlotManager.Application.Commands;
using SlotManager.Application.DTO;

namespace SlotManager.Application.Services
{
    public interface IReservationService
    {
        Task<Guid?> CreateAsync(CreateReservation command);
        Task<bool> DeleteAsync(DeleteReservation command);
        Task<ReservationDto> GetAsync(Guid id);
        Task<IEnumerable<ReservationDto>> GetAllWeeklyAsync();
        Task<bool> UpdateAsync(ChangeReservationLicensePlate command);
    }
}