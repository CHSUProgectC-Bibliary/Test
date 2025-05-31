using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BookReviewAPI.Data.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ✅ ОБЯЗАТЕЛЬНО!
        public int User_Id { get; set; }
        public string User_name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
