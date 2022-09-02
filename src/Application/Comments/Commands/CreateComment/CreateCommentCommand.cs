using Blog.Application.Common.Exceptions;
using Blog.Application.Common.Interfaces;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.Domain.Exceptions;
using MediatR;

namespace Blog.Application.Comments.Commands.CreateComment;
public record CreateCommentCommand : IRequest<int>
{
    public int PostId { get; set; }
    public string Content { get; set; } = null!;
    public CommentType CommentedBy { get; set; } = default!;
}

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityService _identityService;
    private readonly ICurrentUserService _currentUserService;

    public CreateCommentCommandHandler(IApplicationDbContext context, IIdentityService identityService, ICurrentUserService currentUserService)
    {
        _context = context;
        _identityService = identityService;
        _currentUserService = currentUserService;
    }

    public async Task<int> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var post = await _context.Posts
            .FindAsync(new object[] { request.PostId }, cancellationToken);

        if (post == null)
        {
            throw new NotFoundException(nameof(Comment), request.PostId);
        }
        else if(post.Status!=PostStatus.Approved)
        {
            throw new ForbiddenCommentException(post);  
        }

        var entity = new Comment
        {
            PostId = request.PostId,
            Content = request.Content,
            CreatedOn = DateTime.UtcNow,
            CommentType = request.CommentedBy,
            CommentedBy = await _identityService.GetUserNameAsync(_currentUserService.UserId ?? string.Empty),
        };

        _context.Comments.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
