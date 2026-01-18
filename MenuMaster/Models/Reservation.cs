using System.ComponentModel.DataAnnotations.Schema;

namespace MenuMaster.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public int People { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public virtual Restaurant Restaurant { get; set; }
        public virtual User User { get; set; }
    }
}
