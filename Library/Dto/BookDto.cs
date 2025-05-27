namespace BookReviewAPI.Data.Dto
{

public record BookDto(
    string Category,
    int Book_Id,
    string Section,
    string Title,
    string Author,
    string Description
    );
public record CreateBookDto(
    string Section,
    string Title,
    string Author,
    string Description
    );
public record UpdateBookDto(
    string Section,
    string Title,
    string Author,
    string Description
    );
}
