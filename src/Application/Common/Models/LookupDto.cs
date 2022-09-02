using Blog.Application.Common.Mappings;
using Blog.Domain.Entities;

namespace Blog.Application.Common.Models;

// Note: This is currently just used to demonstrate applying multiple IMapFrom attributes.
public class LookupDto
{ 
    public int Id { get; set; }

    public string? Title { get; set; }
}
