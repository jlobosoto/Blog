using Blog.Application.Comments.Commands.CreateComment;
using Blog.Application.Comments.Commands.DeleteComment;
using Blog.Application.Comments.Commands.UpdateComment;
using Blog.Application.Comments.Queries.GetCommentsWithPagination;
using Blog.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebUI.Controllers;

[Authorize]
public class CommentsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<CommentBriefDto>>> GetCommentsWithPagination([FromQuery] GetCommentsWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateCommentCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateCommentCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteCommentCommand(id));

        return NoContent();
    }
}
