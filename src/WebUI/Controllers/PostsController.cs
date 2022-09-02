using Blog.Application.Posts.Commands.ApprovePosts;
using Blog.Application.Posts.Commands.CreatePosts;
using Blog.Application.Posts.Commands.DeletePosts;
using Blog.Application.Posts.Commands.RejectPosts;
using Blog.Application.Posts.Commands.UpdatePosts;
using Blog.Application.Posts.Queries.GetOwnPosts;
using Blog.Application.Posts.Queries.GetPendingPosts;
using Blog.Application.Posts.Queries.GetPosts;
using Blog.Application.Posts.Queries.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebUI.Controllers;

[Authorize]
public class PostsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PostsVm>> Get()
    {
        return await Mediator.Send(new GetPostsQuery());
    }

    [HttpGet("GetPendingPosts")]
    public async Task<ActionResult<PostsVm>> GetPendingPosts()
    {
        return await Mediator.Send(new GetPendingPostsQuery());
    }

    [HttpGet("GetOwnPosts")]
    public async Task<ActionResult<PostsVm>> GetOwnPosts()
    {
        return await Mediator.Send(new GetOwnPostsQuery());
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreatePostsCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdatePostsCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPut("ApprovePost")]
    public async Task<ActionResult> Approve(int id, ApprovePostsCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPut("RejectPost")]
    public async Task<ActionResult> Reject(int id, RejectPostsCommand command)
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
        await Mediator.Send(new DeletePostsCommand(id));

        return NoContent();
    }
}
