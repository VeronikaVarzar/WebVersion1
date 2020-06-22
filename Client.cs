using System;
using System.Collections.Generic;

namespace LibraryWebApplication
{
    public partial class Client
    {
        public Client()
        {
            ParkingSpaces = new HashSet<ParkingSpace>();
            RoomsForRent = new HashSet<RoomForRent>();
        }

        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string PassportId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int PhoneNumber { get; set; }

        public virtual ICollection<ParkingSpace> ParkingSpaces { get; set; }
        public virtual ICollection<RoomForRent> RoomsForRent { get; set; }
    }
}
