using ContactBook.Commands;
using ContactBook.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactBookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactBookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateUser", Name = "CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(user);
        }

        [HttpPost("UpdateUser", Name = "UpdateUser")]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(user);
        }

        [HttpGet("GetUserAddress", Name = "GetUserAddress")]
        public async Task<IActionResult> GetUserAddress([FromQuery] string userId, [FromQuery] string state)
        {
            var query = new AddressByStateQuery{
                UserId = userId,
                State = state
            };
            var address = await _mediator.Send(query);
            return Ok(address);
        }

        [HttpGet("GetUserContact", Name = "GetUserContact")]
        public async Task<IActionResult> GetUserContact([FromQuery] string userId, [FromQuery] string contactType)
        {
            var query = new ContactByTypeQuery
            {
                UserId = userId,
                ContactType = contactType
            };
            var contact = await _mediator.Send(query);
            return Ok(contact);
        }
    }
}
