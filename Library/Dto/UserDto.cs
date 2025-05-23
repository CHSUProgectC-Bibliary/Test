namespace BookReviewAPI.Data.Dto
{
    public record UserDto(
        int User_Id,
        string User_name,
        string Email,
        string Password
    );
    public record CreateUserDto(
        string User_name,
        string Email,
        string Password
        );
    public record UpdateUserDto(
        string User_name,
        string Email,
        string Password
        );
}
