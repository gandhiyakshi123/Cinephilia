using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Cinephilia.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public string Gender { get; set; }

        public string PhoneNumber { get; set; }

        // reference to child object : 1 user has can keep many entertainment paths.

     //   public List<Entertainment> Entertainments{ get; set; }
      //  public List<BrowseBy> BrowseBies{ get; set; }


    }
}