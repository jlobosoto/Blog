using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Security;
using Blog.Application.Posts.Queries.Shared;
using Blog.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Posts.Queries.GetPendingPosts;
[Authorize (Roles="Editor")]
public record GetPendingPostsQuery : IRequest<PostsVm>
{

};

public class GetPedingPostsQueryHandler : IRequestHandler<GetPendingPostsQuery, PostsVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPedingPostsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PostsVm> Handle(GetPendingPostsQuery request, CancellationToken cancellationToken)
    {
        return new PostsVm
        {

            Posts = await _context.Posts
                .AsNoTracking()
                .Where(x=>x.Status==PostStatus.PendingApproval)
                .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Id)
                .ToListAsync(cancellationToken)
        };
    }
}
