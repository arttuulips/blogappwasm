using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface IBlogPostDao
{
    Task<BlogPost> CreateAsync(BlogPost blogPost);
    Task<IEnumerable<BlogPost>> GetAsync(SearchBlogPostParametersDto searchParameters);
    Task UpdateAsync(BlogPost blogPost);
    Task<BlogPost> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}