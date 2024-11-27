using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class BlogPostLogic : IBlogPostLogic
{
    private readonly IBlogPostDao blogPostDao;
    private readonly IUserDao userDao;

    public BlogPostLogic(IBlogPostDao blogPostDao, IUserDao userDao)
    {
        this.blogPostDao = blogPostDao;
        this.userDao = userDao;
    }

    public async Task<BlogPost> CreateAsync(BlogPostCreationDto dto)
    {
        User? user = await userDao.GetByIdAsync(dto.AuthorId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.AuthorId} was not found.");
        }

        ValidateBlogPost(dto);
        BlogPost blogPost = new BlogPost(user, dto.Title, dto.Content, dto.Country);
        BlogPost created = await blogPostDao.CreateAsync(blogPost);
        return created;
    }

    public Task<IEnumerable<BlogPost>> GetAsync(SearchBlogPostParametersDto searchParameters)
    {
        return blogPostDao.GetAsync(searchParameters);
    }

    private void ValidateBlogPost(BlogPostCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        if (string.IsNullOrEmpty(dto.Content)) throw new Exception("Content cannot be empty.");
        if (string.IsNullOrEmpty(dto.Country)) throw new Exception("Country cannot be empty.");
        // other validation stuff
    }
    
    public async Task UpdateAsync(BlogPostUpdateDto dto)
    {
        BlogPost? existing = await blogPostDao.GetByIdAsync(dto.Id);

        if (existing == null)
        {
            throw new Exception($"BlogPost with ID {dto.Id} not found!");
        }

        User? user = null;
        if (dto.AuthorId != null)
        {
            user = await userDao.GetByIdAsync((int)dto.AuthorId);
            if (user == null)
            {
                throw new Exception($"User with id {dto.AuthorId} was not found.");
            }
        }
        

        User userToUse = user ?? existing.Author;
        string titleToUse = dto.Title ?? existing.Title;
        string contentToUse = dto.Content ?? existing.Content;
        string countryToUse = dto.Country ?? existing.Country;
        bool publishedToUse = dto.IsPublished ?? existing.IsPublished;
    
        BlogPost updated = new (userToUse, titleToUse, contentToUse, countryToUse)
        {
            IsPublished = publishedToUse,
            Id = existing.Id,
        };

        ValidateBlogPost(updated);

        await blogPostDao.UpdateAsync(updated);
    }

    private void ValidateBlogPost(BlogPost dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        if (dto.Content == null) throw new Exception("Content cannot be empty.");
    }
    
    public async Task DeleteAsync(int id)
    {
        BlogPost? blogPost = await blogPostDao.GetByIdAsync(id);
        if (blogPost == null)
        {
            throw new Exception($"BlogPost with ID {id} was not found!");
        }

        if (!blogPost.IsPublished)
        {
            throw new Exception("Cannot delete un-published BlogPost!");
        }

        await blogPostDao.DeleteAsync(id);
    }
    
}