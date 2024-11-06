﻿using System;
using System.Collections.Generic;

namespace FlyingDutchmanAirlines.Repositories.Entities;

public class Airport
{
    public int AirportId { get; set; }

    public string City { get; set; } = null!;

    public string Iata { get; set; } = null!;

    public virtual ICollection<Flight> FlightDestinationNavigations { get; set; } = new List<Flight>();

    public virtual ICollection<Flight> FlightOriginNavigations { get; set; } = new List<Flight>();
}
