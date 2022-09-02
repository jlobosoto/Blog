using Blog.Domain.Extensions;

namespace Blog.Domain.Exceptions;
public class NotEditablePostException : Exception
{
    public NotEditablePostException(Post post)
       : base($"Posts with {post.Status.GetDescription()} status are not available to edit.")
    {
    }
}