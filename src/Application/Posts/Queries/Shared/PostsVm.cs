namespace Blog.Application.Posts.Queries.Shared;
public class PostsVm
{
    public IList<PostDto> Posts { get; set; } = new List<PostDto>();
}
