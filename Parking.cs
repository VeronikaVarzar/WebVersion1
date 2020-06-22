using System;
using System.Collections.Generic;

namespace LibraryWebApplication
{
    public partial class Parking
    {
        public Parking()
        {
            ParkingSpaces = new HashSet<ParkingSpace>();
        }

        public int Id { get; set; }
        public int HotelId { get; set; }
        public string Number { get; set; }
        public int StateId { get; set; }

        public virtual Hotel Hotel { get; set; }
        public virtual State State { get; set; }
        public virtual ICollection<ParkingSpace> ParkingSpaces { get; set; }
    }
}
