// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeatherDemo.Infrastructure.Data.SQLite;

#nullable disable

namespace WeatherDemo.Infrastructure.Data.SQLite.Migrations
{
    [DbContext(typeof(WeatherDemoDbContext))]
    partial class WeatherDemoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("WeatherDemo.Domain.Entities.GeoLocation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AltNamesCsv")
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("GeoLocation", (string)null);
                });

            modelBuilder.Entity("WeatherDemo.Domain.Entities.GeoLocation", b =>
                {
                    b.OwnsOne("WeatherDemo.Domain.ValueObjects.GeoPoint", "Point", b1 =>
                        {
                            b1.Property<Guid>("GeoLocationId")
                                .HasColumnType("TEXT");

                            b1.Property<decimal>("Lat")
                                .HasColumnType("TEXT");

                            b1.Property<decimal>("Lon")
                                .HasColumnType("TEXT");

                            b1.HasKey("GeoLocationId");

                            b1.ToTable("GeoLocation");

                            b1.WithOwner()
                                .HasForeignKey("GeoLocationId");
                        });

                    b.Navigation("Point");
                });
#pragma warning restore 612, 618
        }
    }
}
