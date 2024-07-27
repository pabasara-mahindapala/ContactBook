using ContactBook.Commands;
using ContactBook.Queries;
using ContactBook.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactBookController : ControllerBase
    {
        private readonly IUserWriteService _userWriteService;
        private readonly IUserReadService _userReadService;

        public ContactBookController(
            IUserWriteService userWriteService,
            IUserReadService userReadService
        )
        {
            _userWriteService = userWriteService;
            _userReadService = userReadService;
        }

        [HttpPost("CreateUser", Name = "CreateUser")]
        public IActionResult CreateUser(CreateUserCommand command)
        {
            var user = _userWriteService.HandleCreateUserCommand(command);
            return Ok(user);
        }

        [HttpPost("UpdateUser", Name = "UpdateUser")]
        public IActionResult UpdateUser(UpdateUserCommand command)
        {
            var user = _userWriteService.HandleUpdateUserCommand(command);
            return Ok(user);
        }

        [HttpGet("GetUserAddress", Name = "GetUserAddress")]
        public IActionResult GetUserAddress([FromQuery] string userId, [FromQuery] string state)
        {
            var query = new AddressByStateQuery{
                UserId = userId,
                State = state
            };
            var address = _userReadService.Handle(query);
            return Ok(address);
        }

        [HttpGet("GetUserContact", Name = "GetUserContact")]
        public IActionResult GetUserContact([FromQuery] string userId, [FromQuery] string contactType)
        {
            var query = new ContactByTypeQuery
            {
                UserId = userId,
                ContactType = contactType
            };
            var contact = _userReadService.Handle(query);
            return Ok(contact);
        }
    }
}
