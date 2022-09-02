using Blog.Domain.Extensions;

namespace Blog.Domain.Exceptions;
public class ForbiddenCommentException : Exception
{
    public ForbiddenCommentException(Post post)
        : base($"Comment is forbidden for posts with {post.Status.GetDescription()} status.")
    {
    }
}
