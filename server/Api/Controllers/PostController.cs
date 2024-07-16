using Domain.User.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace Api.Controllers;

[Route("api/posts")]
public class PostController : ApiController
{
    protected PostController(ISender mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPosts()
    {
        // GetPostsQuery query = new();
        // ErrorOr<IEnumerable<Post>> result = await Invoke<IEnumerable<Post>>(query);
        // return result.Match(
        //     posts => Ok(_mapper.Map<IEnumerable<PostResponse>>(posts)),
        //     errors => Problem(errors)
        // ) ?? Problem("An unexpected error occurred");

        IReadOnlyList<Post> list = new List<Post>();
        return Ok(list);
    }
}