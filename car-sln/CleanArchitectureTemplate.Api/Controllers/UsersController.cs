using CleanArchitectureTemplate.Application.DTO;
using CleanArchitectureTemplate.Application.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureTemplate.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{companyId}")]
    public async Task<IActionResult> GetAll(long companyId, [FromQuery] int page = 1, [FromQuery] int limit = 5)
    {
        try
        {
            PagedResult<UsersResponse> records = await _mediator.Send(new GetUsersQuery(companyId, page, limit));
            return Ok(records);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return StatusCode(500, new { ErrorMessage = e.Message });
        }
    }

    [HttpGet("{companyId}/{id}")]
    public async Task<IActionResult> GetById(long companyId, long id)
    {
        try
        {
            var record = await _mediator.Send(new GetUserQuery(companyId, id));
            return Ok(new { record });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return StatusCode(500, new { ErrorMessage = e.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommand command)
    {
        try
        {
            var response = await _mediator.Send(command);
            if (response.IsFailure)
            {
                return BadRequest(response.Error);
            }
            return Ok(new { response.IsSuccess });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return StatusCode(500, new { ErrorMessage = e.Message });
        }
    }

    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
    {
        try
        {
            var response = await _mediator.Send(command);
            if (response.IsFailure)
            {
                return BadRequest(response.Error);
            }
            return Ok(new { response.IsSuccess, response.Value });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return StatusCode(500, new { ErrorMessage = e.Message });
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateUserCommand command)
    {
        try
        {
            var response = await _mediator.Send(command);
            if (response.IsFailure)
            {
                return BadRequest(response.Error);
            }
            return Ok(new { response.IsSuccess });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return StatusCode(500, new { ErrorMessage = e.Message });
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(DeleteUserCommand command)
    {
        try
        {
            var response = await _mediator.Send(command);
            if (response.IsFailure)
            {
                return BadRequest(response.Error);
            }
            return Ok(new { response.IsSuccess });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return StatusCode(500, new { ErrorMessage = e.Message });
        }
    }
}
