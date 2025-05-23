using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookReviewAPI.Data.Entities
{
    public class Book
    {
        [Key]
        public int Book_Id { get; set; }

        public string Section { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

    }
}
