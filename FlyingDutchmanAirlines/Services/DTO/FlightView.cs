namespace FlyingDutchmanAirlines.Services.DTO;

public record FlightView(int FlightNumber, AirportDto Origin, AirportDto Destination);