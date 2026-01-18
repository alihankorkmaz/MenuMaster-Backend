namespace MenuMaster.Dtos
{
    public class CreateReviewDto
    {
        public int RestaurantId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
