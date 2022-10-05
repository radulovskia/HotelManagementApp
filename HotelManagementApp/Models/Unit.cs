using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementApp.Models
{
    public class Unit
    {
        [Key]
        public int UnitId { get; set; }
        [Required]
        [Display(Name = "Unit")]
        public string Name { get; set; }
        public string Address { get; set; }
        [Display(Name = "City")]
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public virtual List<Room> Rooms { get; set; }
        public Unit()
        {
            Rooms = new List<Room>();
        }
    }
}