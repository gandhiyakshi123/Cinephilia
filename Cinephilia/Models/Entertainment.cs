namespace Cinephilia.Models
{
    public class Entertainment
    {
        public int EntertainmentId { get; set; }

        public string Category { get; set; }

        public int UserId { get; set; }


        // parent model references 
        public User User { get; set; }
        //Child refernce
        public List<BrowseBy> BrowseBies { get; set; }  



    }
}
