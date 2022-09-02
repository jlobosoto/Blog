using Blog.Application.Common.Exceptions;
using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Security;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.Domain.Exceptions;
using MediatR;

namespace Blog.Application.Posts.Commands.UpdatePosts;
[Authorize(Roles = "Writer")]
public record UpdatePostsCommand : IRequest
{
    public int Id { get; init; }
    public string Title { get; init; } = null!;
    public string Content { get; set; } = null!;
}

public class UpdatePostsCommandHandler : IRequestHandler<UpdatePostsCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdatePostsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdatePostsCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Posts
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Post), request.Id);
        }

        if(entity.Editable)
        {
            entity.Title = request.Title;
            entity.Content = request.Content;
            entity.Status = PostStatus.PendingApproval;
            entity.Editable = false;
            await _context.SaveChangesAsync(cancellationToken);
        }
        else
        {
            throw new NotEditablePostException(entity);
        }

        

        return Unit.Value;
    }
}
