using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace FileData.DAOs;

public class BlogPostFileDao : IBlogPostDao
{
    private readonly FileContext context;

    public BlogPostFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<BlogPost> CreateAsync(BlogPost blogPost)
    {
        int id = 1;
        if (context.BlogPosts.Any())
        {
            id = context.BlogPosts.Max(t => t.Id);
            id++;
        }

        blogPost.Id = id;
        
        context.BlogPosts.Add(blogPost);
        context.SaveChanges();

        return Task.FromResult(blogPost);
    }
    
    public Task<IEnumerable<BlogPost>> GetAsync(SearchBlogPostParametersDto searchParams)
    {
        IEnumerable<BlogPost> result = context.BlogPosts.AsEnumerable();

        if (!string.IsNullOrEmpty(searchParams.Username))
        {
            // we know username is unique, so just fetch the first
            result = context.BlogPosts.Where(blogPost =>
                blogPost.Author.UserName.Equals(searchParams.Username, StringComparison.OrdinalIgnoreCase));
        }

        if (searchParams.UserId != null)
        {
            result = result.Where(t => t.Author.Id == searchParams.UserId);
        }

        if (searchParams.PublishedStatus != null)
        {
            result = result.Where(t => t.IsPublished == searchParams.PublishedStatus);
        }

        if (!string.IsNullOrEmpty(searchParams.TitleContains))
        {
            result = result.Where(t =>
                t.Title.Contains(searchParams.TitleContains, StringComparison.OrdinalIgnoreCase));
        }
        
        if (!string.IsNullOrEmpty(searchParams.ContentContains))
        {
            result = result.Where(t =>
                t.Content.Contains(searchParams.ContentContains, StringComparison.OrdinalIgnoreCase));
        }
        
        if (!string.IsNullOrEmpty(searchParams.CountryContains))
        {
            result = result.Where(t =>
                t.Country.Contains(searchParams.CountryContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(result);
    }
    
    
    public Task<BlogPost?> GetByIdAsync(int blogPostId)
    {
        BlogPost? existing = context.BlogPosts.FirstOrDefault(t => t.Id == blogPostId);
        return Task.FromResult(existing);
    }
    
    public Task UpdateAsync(BlogPost toUpdate)
    {
        BlogPost? existing = context.BlogPosts.FirstOrDefault(todo => todo.Id == toUpdate.Id);
        if (existing == null)
        {
            throw new Exception($"BlogPost with id {toUpdate.Id} does not exist!");
        }

        context.BlogPosts.Remove(existing);
        context.BlogPosts.Add(toUpdate);
    
        context.SaveChanges();
    
        return Task.CompletedTask;
    }
    
    public Task DeleteAsync(int id)
    {
        BlogPost? existing = context.BlogPosts.FirstOrDefault(todo => todo.Id == id);
        if (existing == null)
        {
            throw new Exception($"BlogPost with id {id} does not exist!");
        }

        context.BlogPosts.Remove(existing); 
        context.SaveChanges();
    
        return Task.CompletedTask;
    }
}