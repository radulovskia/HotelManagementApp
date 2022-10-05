using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementApp.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        [Required]
        [Display(Name = "City")]
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Room> Rooms { get; set; }
        public virtual List<Unit> Units { get; set; }
        public City()
        {
            Rooms = new List<Room>();
            Units = new List<Unit>();
        }
    }
}