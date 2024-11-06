using System;
using System.Collections.Generic;

namespace FlyingDutchmanAirlines.Repositories.Entities;

public class Customer
{
    public int CustomerId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
