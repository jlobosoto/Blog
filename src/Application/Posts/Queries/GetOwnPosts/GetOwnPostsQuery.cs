using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Security;
using Blog.Application.Posts.Queries.Shared;
using Blog.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Posts.Queries.GetOwnPosts;
[Authorize(Roles = "Writer")]
public record GetOwnPostsQuery : IRequest<PostsVm>
{

};

public class GetOwnPostsQueryHandler : IRequestHandler<GetOwnPostsQuery, PostsVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;
    private readonly ICurrentUserService _currentUserService;

    public GetOwnPostsQueryHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _identityService = identityService;
        _currentUserService = currentUserService;
    }

    public async Task<PostsVm> Handle(GetOwnPostsQuery request, CancellationToken cancellationToken)
    {
        var user = await _identityService.GetUserNameAsync(_currentUserService.UserId ?? String.Empty);
        return new PostsVm
        {

            Posts = await _context.Posts
                .AsNoTracking()
                .Where(x => x.CreatedBy==user)
                .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Id)
                .ToListAsync(cancellationToken)
        };
    }
}
