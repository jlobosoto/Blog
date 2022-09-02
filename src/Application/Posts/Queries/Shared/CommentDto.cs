using AutoMapper;
using Blog.Application.Common.Mappings;
using Blog.Domain.Entities;
using Blog.Domain.Extensions;

namespace Blog.Application.Posts.Queries.Shared;
public class CommentDto : IMapFrom<Comment>
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public string Content { get; set; } = null!;
    public DateTime CreatedOn { get; set; } = default!;
    public string CommentType { get; set; } = default!;
    public string CommentedBy { get; set; } = default!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Comment, CommentDto>()
            .ForMember(d => d.CommentType, opt => opt.MapFrom(s => s.CommentType.GetDescription()));
    }
}
