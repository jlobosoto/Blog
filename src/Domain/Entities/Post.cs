namespace Blog.Domain.Entities;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime CreatedOn { get; set; }=default!;
    public PostStatus Status { get; set; } = default;
    public IList<Comment> Comments { get; set; } = null!;
    public bool Editable { get; set; } = default;
    public string CreatedBy { get; set; } = null!;
}
