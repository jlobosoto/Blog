using AutoMapper;
using Blog.Application.Common.Mappings;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.Domain.Extensions;

namespace Blog.Application.Posts.Queries.Shared;
public class PostDto : IMapFrom<Post>
{
    public PostDto()
    {
        Comments = new List<CommentDto>();
    }

    public int Id { get; set; }

    public string Title { get; set; } = default!;

    public string Content { get; set; }= default!;

    public string Status { get; set; } = null!;
    public string CreatedBy { get; set; } = null!;
    public DateTime CreatedOn { get; set; } = default!;
    public IList<CommentDto> Comments { get; set; }
    

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Post, PostDto>()
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.GetDescription()));
    }
}
