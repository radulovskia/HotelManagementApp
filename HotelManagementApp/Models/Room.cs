using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelManagementApp.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string Number { get; set; }
        public int Price { get; set; }
        [Display(Name = "Preview")]
        public string RoomArtUrl { get; set; }
        public virtual Guest Guest { get; set; }
        [Display(Name = "Unit")]
        public int UnitId { get; set; }
        public virtual Unit Unit { get; set; }
        public object City { get; internal set; }
    }
}