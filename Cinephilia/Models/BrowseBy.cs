namespace Cinephilia.Models
{
    public class BrowseBy
    {
        public int BrowseById { get; set; }
        public string Genre { get; set; }
        public int Language { get; set; }
        public int Rating { get; set; }
        public int UserId { get; set; }
        public int EntertainmentId { get; set; }

        //Parent model refernces
        public User User { get; set; }
        public Entertainment Entertainment { get; set; }


    }
}
