using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Security;
using Blog.Application.Posts.Queries.Shared;
using Blog.Domain.Enums;
using Blog.Domain.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Posts.Queries.GetPosts;
[Authorize]
public record GetPostsQuery : IRequest<PostsVm>
{

};

public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, PostsVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;
    private readonly ICurrentUserService _currentUserService;

    public GetPostsQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService, IIdentityService identityService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
        _identityService = identityService;
    }

    public async Task<PostsVm> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        var username = await _identityService.GetUserNameAsync(_currentUserService.UserId ?? string.Empty);

        var result = new PostsVm
        {
            Posts = await _context.Posts
                .AsNoTracking()
                .Where(x => x.Status == PostStatus.Approved)
                .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Id)
                .ToListAsync(cancellationToken)
        };
                

        foreach (var post in result.Posts)
        {
            if (post.CreatedBy != username)
            {
               post.Comments= post.Comments.Where(x => x.CommentType != CommentType.ByEditorRejected.GetDescription()).ToList();
            }

        }

        return result;
    }
}
