using HotelManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagementApp.Models
{
    public class AddToUnitModel
    {
        public int UnitId { get; set; }
        public int RoomId { get; set; }
        public List<Room> Rooms { get; set; }
        public AddToUnitModel()
        {
            Rooms = new List<Room>();
        }
    }
}