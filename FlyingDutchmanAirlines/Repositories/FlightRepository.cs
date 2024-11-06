using FlyingDutchmanAirlines.Repositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlyingDutchmanAirlines.Repositories;

public class FlightRepository(FlyingDutchmanAirlinesDbContext dbContext) : IFlightRepository
{
    public List<Flight> GetFlights()
    {
        return dbContext.Flights
            .Include(f => f.OriginNavigation)
            .Include(f => f.DestinationNavigation)
            .ToList();
    }
}