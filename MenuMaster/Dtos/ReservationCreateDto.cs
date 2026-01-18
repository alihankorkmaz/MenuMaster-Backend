namespace MenuMaster.Dtos
{
    public class ReservationCreateDto
    {
        public int RestaurantId { get; set; }
        public int People { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }
}
