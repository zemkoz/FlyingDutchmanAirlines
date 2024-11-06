using FlyingDutchmanAirlines.Repositories;
using FlyingDutchmanAirlines.Repositories.Entities;
using JetBrains.Annotations;
using Moq;
using Moq.EntityFrameworkCore;

namespace FlyingDutchmanAirlinesTests.Repositories;

[TestSubject(typeof(FlightRepository))]
public class FlightRepositoryTests
{
    private readonly FlightRepository _sut;
    
    private readonly Mock<FlyingDutchmanAirlinesDbContext> _dbContextMock;
    
    public FlightRepositoryTests()
    {
        _dbContextMock = new Mock<FlyingDutchmanAirlinesDbContext>();
        _sut = new FlightRepository(_dbContextMock.Object);
    }

    [Fact]
    public void GetFlightsAsList()
    {
        // Arrange
        var firstFlight = new Flight();
        firstFlight.FlightNumber = 123;
        firstFlight.OriginNavigation = new Airport();
        firstFlight.DestinationNavigation = new Airport();
        
        var secondFlight = new Flight();
        secondFlight.FlightNumber = 456;
        secondFlight.OriginNavigation = new Airport();
        secondFlight.DestinationNavigation = new Airport();
        
        _dbContextMock.Setup(m => m.Flights)
            .ReturnsDbSet( [firstFlight, secondFlight] );
        
        // Act
        var flights = _sut.GetFlights();
        
        // Assert
        Assert.Equal(2, flights.Count);
        Assert.Equal(123, flights[0].FlightNumber);
        Assert.NotNull(flights[0].OriginNavigation);
        Assert.NotNull(flights[0].DestinationNavigation);
        
        Assert.Equal(456, flights[1].FlightNumber);
    }
}