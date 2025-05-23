using System.ComponentModel.DataAnnotations;

namespace BookReviewAPI.Data.Entities
{
    public class Review
    {
        [Key]
        public int Review_Id { get; set; }

        public string Book_Title_Auth { get; set; }

        public DateOnly Review_Date { get; init; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        public int Book_Id { get; set; }
        public Book Book { get; set; }

        public int User_Id { get; set; }
        public User User { get; set; }
    }
}
