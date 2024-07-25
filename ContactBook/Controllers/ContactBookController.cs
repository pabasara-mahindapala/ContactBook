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

        [HttpPost(Name = "CreateUser")]
        public IActionResult CreateUser(CreateUserCommand command)
        {
            var user = _userAggregate.HandleCreateUserCommand(command);
            return CreatedAtRoute("GetWeatherForecast", new { id = user.Id }, user);
        }

        [HttpPut(Name = "UpdateUser")]
        public IActionResult UpdateUser(UpdateUserCommand command)
        {
            var user = _userAggregate.HandleUpdateUserCommand(command);
            return CreatedAtRoute("GetWeatherForecast", new { id = user.Id }, user);
        }

        [HttpGet("GetUserAddress/{userId}", Name = "GetUserAddress")]
        public IActionResult GetUserAddress([FromRoute] string userId)
        {
            var userContact = _userReadRepository.GetUserAddress(userId);
            return Ok(userContact);
        }
    }
}
