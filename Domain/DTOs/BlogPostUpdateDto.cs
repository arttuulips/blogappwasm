namespace Domain.DTOs;

public class BlogPostUpdateDto
{
    public int Id { get; }
    public int? AuthorId { get; set; }
    public string? Title { get; set; }
    public string Content { get; set; }
    public string Country { get; set; }
    public bool? IsPublished { get; set; }

    public BlogPostUpdateDto(int id)
    {
        Id = id;
    }
}