using Blog.Application.Common.Exceptions;
using Blog.Application.Common.Interfaces;
using Blog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Posts.Commands.DeletePosts;

public record DeletePostsCommand(int Id) : IRequest;

public class DeletePostsCommandHandler : IRequestHandler<DeletePostsCommand>
{
    private readonly IApplicationDbContext _context;

    public DeletePostsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeletePostsCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Posts
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Post), request.Id);
        }

        _context.Posts.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
