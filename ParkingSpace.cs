using System;
using System.Collections.Generic;

namespace LibraryWebApplication
{
    public partial class ParkingSpace
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime DateOfEviction { get; set; }
        public int ParkingId { get; set; }

        public virtual Client Client { get; set; }
        public virtual Parking Parking { get; set; }
    }
}
