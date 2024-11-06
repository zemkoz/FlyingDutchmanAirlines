using FlyingDutchmanAirlines.Repositories;
using FlyingDutchmanAirlines.Repositories.Entities;
using FlyingDutchmanAirlines.Services;
using FlyingDutchmanAirlines.Services.DTO;
using JetBrains.Annotations;
using Moq;

namespace FlyingDutchmanAirlinesTests.Services;

[TestSubject(typeof(FlightService))]
public class FlightServiceTests
{
    private readonly FlightService _sut;

    private readonly Mock<IFlightRepository> _flightRepositoryStub;

    public FlightServiceTests()
    {
        _flightRepositoryStub = new Mock<IFlightRepository>();
        _sut = new FlightService(_flightRepositoryStub.Object);
    }

    [Fact]
    public void GetEmptyListWhenNoFlightsAreAvailable()
    {
        _flightRepositoryStub
            .Setup(m => m.GetFlights())
            .Returns([]);
        
        var actualFlights = _sut.GetFlights();
        
        Assert.Empty(actualFlights);
    }
    
    [Fact]
    public void GetAllAvailableFlights()
    {
        // Arrange
        List<Flight> flightsList =
        [
            GivenFlight(111, "AMS", "Amsterdam", "LHR", "London"),
            GivenFlight(222, "NYK", "New York", "LHR", "London"),
            GivenFlight(333, "LHR", "London", "PRG", "Prague")
        ];
        
        _flightRepositoryStub
            .Setup(m => m.GetFlights())
            .Returns(flightsList);
        
        // Act
        var actualFlights = _sut.GetFlights();
        
        // Assert
        Assert.Equal(3, actualFlights.Count);
        ThenEqualsToFlight(flightsList[0], actualFlights[0]);
        ThenEqualsToFlight(flightsList[1], actualFlights[1]);
        ThenEqualsToFlight(flightsList[2], actualFlights[2]);
    }

    private static Flight GivenFlight(int flightNumber, 
        string originIata, 
        string originCity,
        string destinationIata,
        string destinationCity)
    {
        return new Flight
        {
            FlightNumber = flightNumber,
            Origin = 1,
            Destination = 2,
            OriginNavigation = new Airport
            {
                AirportId = 1,
                Iata = originIata,
                City = originCity
            },
            DestinationNavigation = new Airport
            {
                AirportId = 2,
                Iata = destinationIata,
                City = destinationCity
            }
        };
    }

    private static void ThenEqualsToFlight(Flight expectedFlightEntity, FlightView actualFlightView)
    {
        Assert.Equal(expectedFlightEntity.FlightNumber, actualFlightView.FlightNumber);
        Assert.Equal(expectedFlightEntity.OriginNavigation.Iata, actualFlightView.Origin.Code );
        Assert.Equal(expectedFlightEntity.OriginNavigation.City, actualFlightView.Origin.City );
        Assert.Equal(expectedFlightEntity.DestinationNavigation.Iata, actualFlightView.Destination.Code );
        Assert.Equal(expectedFlightEntity.DestinationNavigation.City, actualFlightView.Destination.City );
    }
}