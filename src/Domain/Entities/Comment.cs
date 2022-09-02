

namespace Blog.Domain.Entities;

public class Comment
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public string Content { get; set; }=null!;
    public DateTime CreatedOn { get; set; }=default!;
    public CommentType CommentType { get; set; } = default!;
    public string CommentedBy { get; set; } = null!;

}
