using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeatherDemo.Domain.Entities;

namespace WeatherDemo.Infrastructure.Data.SQLite.EntitiesConfiguration
{
    internal class GeoLocationConfiguration : IEntityTypeConfiguration<GeoLocation>
    {
        public void Configure(EntityTypeBuilder<GeoLocation> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ToTable(nameof(GeoLocation));

            builder.HasKey(e => e.Id);

            builder.OwnsOne(e => e.Point);

            builder.Property(e => e.Name).IsRequired();

            builder.Property(e => e.AltNamesCsv);

            builder.Property(e => e.Country);

            builder.Property(e => e.Created);

        }
    }
}
