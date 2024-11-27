using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IBlogPostLogic
{
    Task<BlogPost> CreateAsync(BlogPostCreationDto dto);
    Task<IEnumerable<BlogPost>> GetAsync(SearchBlogPostParametersDto searchParameters);
    Task UpdateAsync(BlogPostUpdateDto todo);
    Task DeleteAsync(int id);
}