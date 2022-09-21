using SlotManager.Application.Commands;
using SlotManager.Application.DTO;

namespace SlotManager.Application.Services
{
    public interface IReservationService
    {
        Guid? Create(CreateReservation command);
        bool Delete(DeleteReservation command);
        ReservationDto Get(Guid id);
        IEnumerable<ReservationDto> GetAllWeekly();
        bool Update(ChangeReservationLicensePlate command);
    }
}