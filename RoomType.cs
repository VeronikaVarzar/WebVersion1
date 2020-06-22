using System;
using System.Collections.Generic;

namespace LibraryWebApplication
{
    public partial class RoomType
    {
        public RoomType()
        {
            HotelRooms = new HashSet<HotelRoom>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int HotelId { get; set; }
        public decimal Cost { get; set; }
        public int NumberOfSeats { get; set; }
        public bool TiiletAvailability { get; set; }

        public virtual Hotel Hotel { get; set; }
        public virtual ICollection<HotelRoom> HotelRooms { get; set; }
    }
}
