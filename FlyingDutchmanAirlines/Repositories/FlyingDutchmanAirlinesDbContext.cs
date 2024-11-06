using FlyingDutchmanAirlines.Repositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlyingDutchmanAirlines.Repositories;

public class FlyingDutchmanAirlinesDbContext : DbContext
{
    public FlyingDutchmanAirlinesDbContext()
    {
    }

    public FlyingDutchmanAirlinesDbContext(DbContextOptions<FlyingDutchmanAirlinesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Airport> Airports { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            Environment.GetEnvironmentVariable("FLYING_DUTCHMAN_AIRLINES_CONNECTION_STRING")
            );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<Airport>(entity =>
        {
            entity.ToTable("Airport");

            entity.Property(e => e.AirportId)
                .ValueGeneratedNever()
                .HasColumnName("AirportID");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Iata)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("IATA");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__73951ACD5FA4D6CE");

            entity.ToTable("Booking");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Booking__Custome__3D5E1FD2");

            entity.HasOne(d => d.FlightNumberNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.FlightNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__FlightN__3C69FB99");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.FlightNumber);

            entity.ToTable("Flight");

            entity.Property(e => e.FlightNumber).ValueGeneratedNever();

            entity.HasOne(d => d.DestinationNavigation).WithMany(p => p.FlightDestinationNavigations)
                .HasForeignKey(d => d.Destination)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Flight_AirportDestination");

            entity.HasOne(d => d.OriginNavigation).WithMany(p => p.FlightOriginNavigations)
                .HasForeignKey(d => d.Origin)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });
    }
}
