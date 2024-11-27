namespace Domain.Models;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public List<BlogPost> BlogPosts { get; set; } = new List<BlogPost>(); // User's blog posts
}