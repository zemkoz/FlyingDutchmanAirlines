using FlyingDutchmanAirlines.Controllers;
using FlyingDutchmanAirlines.Services;
using FlyingDutchmanAirlines.Services.DTO;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FlyingDutchmanAirlinesTests.Controllers;

[TestSubject(typeof(FlightsController))]
public class FlightsControllerTest
{
    private readonly FlightsController _sut;
    
    private readonly Mock<IFlightService> _flightServiceStub;

    public FlightsControllerTest()
    {
        _flightServiceStub = new Mock<IFlightService>();
        _sut = new FlightsController(_flightServiceStub.Object);
    }

    [Fact]
    public void GetOkResponseWithListOfFlights()
    {
        // Arrange
        List<FlightView> flights = new List<FlightView>
        {
            new FlightView(111, new AirportDto("AMS", "Amsterdam"), new AirportDto("LHR", "London")),
            new FlightView(222, new AirportDto("NYK", "New York"), new AirportDto("LHR", "London")),
            new FlightView(333, new AirportDto("LHR", "London"), new AirportDto("PRG", "Prague"))
        };
        
        _flightServiceStub
            .Setup(m => m.GetFlights())
            .Returns(flights);
        
        // Act
        var result = _sut.GetFlights();
        
        // Assert
        Assert.IsType<OkObjectResult>(result);
        var okResult = (OkObjectResult) result;
        Assert.Equal(flights, okResult.Value);
        
    }
}