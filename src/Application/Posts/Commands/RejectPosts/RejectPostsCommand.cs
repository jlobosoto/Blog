using Blog.Application.Common.Exceptions;
using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Security;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using MediatR;

namespace Blog.Application.Posts.Commands.RejectPosts;
[Authorize (Roles="Editor")]
public record RejectPostsCommand : IRequest
{
    public int Id { get; init; }
    public string comment { get; set; } = null!;
}

public class RejectPostsCommandHandler : IRequestHandler<RejectPostsCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityService _identityService;
    private readonly ICurrentUserService _currentUserService;

    public RejectPostsCommandHandler(IApplicationDbContext context, IIdentityService identityService, ICurrentUserService currentUserService)
    {
        _context = context;
        _identityService = identityService;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(RejectPostsCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Posts
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Post), request.Id);
        }
        entity.Status = PostStatus.Rejected;
        entity.Editable = true;
        var editorComment = new Comment()
        {
            Content = request.comment,
            CommentedBy = await _identityService.GetUserNameAsync(_currentUserService.UserId ?? string.Empty),
            CommentType = CommentType.ByEditorRejected,
            CreatedOn = DateTime.UtcNow,
            PostId = entity.Id
        };
        _context.Comments.Add(editorComment);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
