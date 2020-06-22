using System;
using System.Collections.Generic;

namespace LibraryWebApplication
{
    public partial class State
    {
        public State()
        {
            HotelRooms = new HashSet<HotelRoom>();
            Parking = new HashSet<Parking>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<HotelRoom> HotelRooms { get; set; }
        public virtual ICollection<Parking> Parking { get; set; }
    }
}
