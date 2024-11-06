using FlyingDutchmanAirlines.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlyingDutchmanAirlines.Controllers;

[ApiController]
[Route("api/flights")]
public class FlightsController(IFlightService flightService) : ControllerBase
{
    [HttpGet]
    public ActionResult GetFlights()
    {
        var flights = flightService.GetFlights();
        return Ok(flights);
    }
}