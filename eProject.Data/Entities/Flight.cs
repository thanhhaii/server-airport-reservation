using System;
using System.Collections.Generic;

#nullable disable

namespace eProject.Data.Entities
{
    public class Flight
    {
        public Flight()
        {
            Tickets = new HashSet<Ticket>();
        }

        public Guid FlightId { get; set; }
        public int PlaneId { get; set; }
        public DateTime FlightTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int TotalSeats { get; set; }

        public virtual Plane Plane { get; set; }
        public virtual FlightDetail FlightDetails { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
