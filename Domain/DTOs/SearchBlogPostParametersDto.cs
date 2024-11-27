namespace Domain.DTOs;

public class SearchBlogPostParametersDto
{
    public string? Username { get;}
    public int? UserId { get;}
    public bool? PublishedStatus { get;}
    public string? TitleContains { get;}
    public string? ContentContains { get;}
    public string? CountryContains { get;}

    public SearchBlogPostParametersDto(string? username, int? userId, bool? publishedStatus, string? titleContains, string? contentContains, string? countryContains)
    {
        Username = username;
        UserId = userId;
        PublishedStatus = publishedStatus;
        TitleContains = titleContains;
        ContentContains = contentContains;
        CountryContains = countryContains;
    } 
}