using Blog.Application.Common.Exceptions;
using Blog.Application.Common.Interfaces;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using MediatR;

namespace Blog.Application.Comments.Commands.UpdateComment;
public record UpdateCommentCommand : IRequest
{
    public int Id { get; init; }
    public string Content { get; init; } = null!;
    public CommentType CommentType { get; init; } = default!;
    public string CommentedBy { get; set; } = null!;
}

public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCommentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Comments
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Comment), request.Id);
        }

        entity.Content = request.Content;
        entity.CommentType = request.CommentType;
        entity.CommentedBy = request.CommentedBy;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
