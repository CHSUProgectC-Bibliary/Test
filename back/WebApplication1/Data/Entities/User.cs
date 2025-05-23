using System.ComponentModel.DataAnnotations;
namespace BookReviewAPI.Data.Entities
{
    public class User
    {
        [Key]
        public int User_Id { get; set; }
        public string User_name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
