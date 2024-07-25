using ContactBook.Aggregates;
using ContactBook.Commands;
using ContactBook.Projections;
using ContactBook.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ContactBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactBookController : ControllerBase
    {
        private readonly UserAggregate _userAggregate;
        private readonly UserProjection _userProjection;

        public ContactBookController(
            UserAggregate userAggregate,
            UserProjection userProjection
        )
        {
            _userAggregate = userAggregate;
            _userProjection = userProjection;
        }

        [HttpPost("CreateUser", Name = "CreateUser")]
        public IActionResult CreateUser(CreateUserCommand command)
        {
            var user = _userAggregate.HandleCreateUserCommand(command);
            return Ok(user);
        }

        [HttpPost("UpdateUser", Name = "UpdateUser")]
        public IActionResult UpdateUser(UpdateUserCommand command)
        {
            var user = _userAggregate.HandleUpdateUserCommand(command);
            return Ok(user);
        }

        [HttpGet("GetUserAddress", Name = "GetUserAddress")]
        public IActionResult GetUserAddress([FromBody] AddressByStateQuery query)
        {
            var address = _userProjection.Handle(query);
            return Ok(address);
        }

        [HttpGet("GetUserContact", Name = "GetUserContact")]
        public IActionResult GetUserContact([FromBody] ContactByTypeQuery query)
        {
            var contact = _userProjection.Handle(query);
            return Ok(contact);
        }
    }
}
