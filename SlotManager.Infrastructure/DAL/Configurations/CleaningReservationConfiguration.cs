using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SlotManager.Core.Entities;

namespace SlotManager.Infrastructure.DAL.Configurations
{
    public class CleaningReservationConfiguration : IEntityTypeConfiguration<CleaningReservation>
    {
        public void Configure(EntityTypeBuilder<CleaningReservation> builder)
        {
        }
    }
}
