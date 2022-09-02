using Blog.Application.Common.Exceptions;
using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Security;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using MediatR;

namespace Blog.Application.Posts.Commands.ApprovePosts;
[Authorize (Roles="Editor")]
public record ApprovePostsCommand : IRequest
{
    public int Id { get; init; }
}

public class ApprovePostsCommandHandler : IRequestHandler<ApprovePostsCommand>
{
    private readonly IApplicationDbContext _context;

    public ApprovePostsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(ApprovePostsCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Posts
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Post), request.Id);
        }
        entity.Status = PostStatus.Approved;
        entity.Editable=false;
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
