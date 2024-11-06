using FlyingDutchmanAirlines.Repositories.Entities;

namespace FlyingDutchmanAirlines.Repositories;

public interface IFlightRepository
{
    List<Flight> GetFlights();
}