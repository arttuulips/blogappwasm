namespace Domain.Models;

public class BlogPost
{
    public int Id { get; set; }
    public User Author { get; set; } // Links to the User who created the blog post
    public string Title { get; set; }
    public string Content { get; set; }
    public string Country { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsPublished { get; set; }


    public BlogPost(User author, string title, string content, string country)
    {
        Author = author;
        Title = title;
        Content = content;
        Country = country;
        CreatedAt = DateTime.UtcNow;
        IsPublished = false; // Defaults to false initially
    }
}
