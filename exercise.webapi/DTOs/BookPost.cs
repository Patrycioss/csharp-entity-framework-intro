namespace exercise.webapi.DTOs;

public class BookPost
{
    public string Title { get; set; }
    public BookAuthorPost BookAuthor { get; set; }
}