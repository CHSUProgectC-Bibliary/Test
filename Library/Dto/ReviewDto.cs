namespace BookReviewAPI.Data.Dto
{
    public record ReviewDto(
        int Review_Id,
        int User_Id,
        int Book_id,
        string Book_Title_Auth,
        DateTime Review_Date,
        int Rating,
        string Comment
    );
    public record CreateReviewDto(
        int User_Id,
        int Book_id,
        string Book_Title_Auth,
        DateTime Review_Date,
        int Rating,
        string Comment
        );
    public record UpdateReviewDto(
        int User_Id,
        int Book_id,
        string Book_Title_Auth,
        DateTime Review_Date,
        int Rating,
        string Comment
        );
}
