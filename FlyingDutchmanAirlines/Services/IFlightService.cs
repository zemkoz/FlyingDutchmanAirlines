using FlyingDutchmanAirlines.Services.DTO;

namespace FlyingDutchmanAirlines.Services;

public interface IFlightService
{
    List<FlightView> GetFlights();
}