namespace MenuMaster.Dtos
{
    public class ReservationListDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string RestaurantName { get; set; }
        public DateTime Date { get; set; }
        public int PeopleCount { get; set; }
        public string Status { get; set; }
    }
}
