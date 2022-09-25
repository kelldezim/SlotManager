using Microsoft.EntityFrameworkCore;
using SlotManager.Core.Entities;
using SlotManager.Core.Repositories;
using SlotManager.Core.ValueObjects;

namespace SlotManager.Infrastructure.DAL.Repositories
{
    internal class SqlWeeklyParkingSpotRepository : IWeeklyParkingSpotRepository
    {
        private readonly SlotManagerDbContext _dbContext;

        public SqlWeeklyParkingSpotRepository(SlotManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(WeeklyParkingSpot weeklyParkingSpot)
        {
            await _dbContext.AddAsync(weeklyParkingSpot);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(WeeklyParkingSpot weeklyParkingSpot)
        {
            _dbContext.Remove(weeklyParkingSpot);
            await _dbContext.SaveChangesAsync();
        }

        public Task<WeeklyParkingSpot> GetAsync(ParkingSpotId id)
        {
              return _dbContext.WeeklyParkingSpots
                            .Include(x => x.Reservations)
                            .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<WeeklyParkingSpot>> GetAllAsync()
        {
            var result = await _dbContext.WeeklyParkingSpots
                            .Include(x => x.Reservations)
                            .ToListAsync();

            return result.AsEnumerable();
        }

        public async Task UpdateAsync(WeeklyParkingSpot weeklyParkingSpot)
        {
            _dbContext.Update(weeklyParkingSpot);
            await _dbContext.SaveChangesAsync();
        }
    }
}
