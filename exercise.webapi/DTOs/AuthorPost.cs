namespace exercise.webapi.DTOs;

public class AuthorPost
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public List<string> Books { get; set; }
}