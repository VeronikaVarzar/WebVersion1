using System;
using System.Collections.Generic;

namespace LibraryWebApplication
{
    public partial class Hotel
    {
        public Hotel()
        {
            Parking = new HashSet<Parking>();
            RoomTypes = new HashSet<RoomType>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int TownId { get; set; }
        public int NumberOfStars { get; set; }

        public virtual Town Town { get; set; }
        public virtual ICollection<Parking> Parking { get; set; }
        public virtual ICollection<RoomType> RoomTypes { get; set; }
    }
}
