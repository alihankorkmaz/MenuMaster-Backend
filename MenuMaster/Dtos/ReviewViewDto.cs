namespace MenuMaster.Dtos
{
    public class ReviewViewDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } 
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
    }
}
