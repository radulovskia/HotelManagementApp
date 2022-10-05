using System;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementApp.Models
{
    public class Guest
    {
        [Key]
        public int GuestId { get; set; }
        [Required]
        [Display(Name = "Guest")]
        public string Name { get; set; }
        [Display(Name = "Arrival Date")]
        public DateTime DateFrom { get; set; }
        [Display(Name = "Departure Date")]
        public DateTime DateTo { get; set; }
        public Guest()
        {
            DateFrom = DateTime.MinValue;
            DateTo = DateTime.MinValue;
        }
    }
}