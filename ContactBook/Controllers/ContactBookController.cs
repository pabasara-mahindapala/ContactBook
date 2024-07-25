using ContactBook.Aggregates;
using ContactBook.Commands;
using ContactBook.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ContactBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactBookController : ControllerBase
    {
        private readonly UserAggregate _userAggregate;
        private readonly IUserReadRepository _userReadRepository;

        public ContactBookController(
            UserAggregate userAggregate,
            IUserReadRepository userReadRepository
        )
        {
            _userAggregate = userAggregate;
            _userReadRepository = userReadRepository;
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

        [HttpGet("GetUserAddress/{userId}", Name = "GetUserAddress")]
        public IActionResult GetUserAddress([FromRoute] string userId)
        {
            var userAddress = _userReadRepository.GetUserAddress(userId);
            return Ok(userAddress);
        }

        [HttpGet("GetUserContact/{userId}", Name = "GetUserContact")]
        public IActionResult GetUserContact([FromRoute] string userId)
        {
            var userContact = _userReadRepository.GetUserContact(userId);
            return Ok(userContact);
        }
    }
}
