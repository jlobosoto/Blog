using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Mappings;
using Blog.Application.Common.Models;
using MediatR;

namespace Blog.Application.Comments.Queries.GetCommentsWithPagination;
public record GetCommentsWithPaginationQuery : IRequest<PaginatedList<CommentBriefDto>>
{
    public int PostId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetCommentsWithPaginationQueryHandler : IRequestHandler<GetCommentsWithPaginationQuery, PaginatedList<CommentBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCommentsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CommentBriefDto>> Handle(GetCommentsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Comments
            .Where(x => x.PostId == request.PostId)
            .OrderBy(x => x.CreatedOn)
            .ProjectTo<CommentBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
