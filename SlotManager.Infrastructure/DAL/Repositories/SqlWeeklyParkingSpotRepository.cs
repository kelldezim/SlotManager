using Microsoft.EntityFrameworkCore;
using SlotManager.Core.Entities;
using SlotManager.Core.Repositories;
using SlotManager.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotManager.Infrastructure.DAL.Repositories
{
    internal class SqlWeeklyParkingSpotRepository : IWeeklyParkingSpotRepository
    {
        private readonly SlotManagerDbContext _dbContext;

        public SqlWeeklyParkingSpotRepository(SlotManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(WeeklyParkingSpot weeklyParkingSpot)
        {
            _dbContext.Add(weeklyParkingSpot);
            _dbContext.SaveChanges();
        }

        public void Delete(WeeklyParkingSpot weeklyParkingSpot)
        {
            _dbContext.Remove(weeklyParkingSpot);
            _dbContext.SaveChanges();
        }

        public WeeklyParkingSpot Get(ParkingSpotId id)
        {
              return _dbContext.WeeklyParkingSpots
                            .Include(x => x.Reservations)
                            .SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<WeeklyParkingSpot> GetAll()
        {
            return _dbContext.WeeklyParkingSpots
                            .Include(x => x.Reservations)
                            .ToList();
        }

        public void Update(WeeklyParkingSpot weeklyParkingSpot)
        {
            _dbContext.Update(weeklyParkingSpot);
            _dbContext.SaveChanges();
        }
    }
}
