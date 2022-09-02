using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Security;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using MediatR;

namespace Blog.Application.Posts.Commands.CreatePosts;
[Authorize(Roles = "Writer")]
public record CreatePostsCommand : IRequest<int>
{
    public string Title { get; init; } = default!;
    public string Content { get; init; } = default!;
}

public class CreatePostsCommandHandler : IRequestHandler<CreatePostsCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityService _identityService;
    private readonly ICurrentUserService _currentUserService;

    public CreatePostsCommandHandler(IApplicationDbContext context, IIdentityService identityService, ICurrentUserService currentUserService)
    {
        _context = context;
        _identityService = identityService;
        _currentUserService = currentUserService;
    }

    public async Task<int> Handle(CreatePostsCommand request, CancellationToken cancellationToken)
    {
        var entity = new Post()
        {
            Title = request.Title,
            Content = request.Content,
            CreatedOn = DateTime.UtcNow,
            Status = PostStatus.PendingApproval,
            Editable = false,
            CreatedBy = await _identityService.GetUserNameAsync(_currentUserService.UserId ?? String.Empty)
    };

        _context.Posts.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
