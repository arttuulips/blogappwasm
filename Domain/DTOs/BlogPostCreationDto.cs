namespace Domain.DTOs;

public class BlogPostCreationDto
{
    public int AuthorId { get; }
    public string Title { get; }
    public string Content { get; }
    public string Country { get; }

    public BlogPostCreationDto(int authorId, string title, string content, string country)
    {
        AuthorId = authorId;
        Title = title;
        Content = content;
        Country = country;
    }
}