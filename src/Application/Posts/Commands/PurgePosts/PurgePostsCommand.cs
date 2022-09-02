using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Security;
using MediatR;

namespace Blog.Application.Posts.Commands.PurgePosts;
[Authorize(Roles = "Administrator")]
[Authorize(Policy = "CanPurge")]
public record PurgePostsCommand : IRequest;

public class PurgePostsCommandHandler : IRequestHandler<PurgePostsCommand>
{
    private readonly IApplicationDbContext _context;

    public PurgePostsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(PurgePostsCommand request, CancellationToken cancellationToken)
    {
        _context.Posts.RemoveRange(_context.Posts);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
