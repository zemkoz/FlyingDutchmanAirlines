using FlyingDutchmanAirlines.Repositories;
using FlyingDutchmanAirlines.Repositories.Entities;
using FlyingDutchmanAirlines.Services.DTO;

namespace FlyingDutchmanAirlines.Services;

public class FlightService(IFlightRepository flightRepository) : IFlightService
{
    public List<FlightView> GetFlights()
    {
        return flightRepository.GetFlights()
            .Select(MapFlightToFlightView)
            .ToList();
    }

    private static FlightView MapFlightToFlightView(Flight flight)
    {
        var originAirport = new AirportDto(
            flight.OriginNavigation.Iata,
            flight.OriginNavigation.City
            );
        var destinationAirport = new AirportDto(
            flight.DestinationNavigation.Iata, 
            flight.DestinationNavigation.City
            );
        
        return new FlightView(
            FlightNumber: flight.FlightNumber,
            Origin: originAirport,
            Destination: destinationAirport
        );
    }
}