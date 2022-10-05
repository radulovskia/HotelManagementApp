using HotelManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagementApp.Models
{
    public class AddToRoomModel
    {
        public int RoomId { get; set; }
        public int GuestId { get; set; }
        public List<Guest> Guests { get; set; }
        public AddToRoomModel()
        {
            Guests = new List<Guest>();
        }
    }
}