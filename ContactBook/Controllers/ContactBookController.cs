using ContactBook.Aggregates;
using ContactBook.Commands;
using ContactBook.Projections;
using ContactBook.Projectors;
using ContactBook.Queries;
using ContactBook.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ContactBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactBookController : ControllerBase
    {
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly IUserProjector _userProjector;

        public ContactBookController(
            IUserWriteRepository userWriteRepository,
            IUserReadRepository userReadRepository,
            IUserProjector userProjector
        )
        {
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
            _userProjector = userProjector;
        }

        [HttpPost("CreateUser", Name = "CreateUser")]
        public IActionResult CreateUser(CreateUserCommand command)
        {
            var userAggregate = new UserAggregate(_userWriteRepository, _userProjector);
            var user = userAggregate.HandleCreateUserCommand(command);
            return Ok(user);
        }

        [HttpPost("UpdateUser", Name = "UpdateUser")]
        public IActionResult UpdateUser(UpdateUserCommand command)
        {
            var userAggregate = new UserAggregate(_userWriteRepository, _userProjector);
            var user = userAggregate.HandleUpdateUserCommand(command);
            return Ok(user);
        }

        [HttpGet("GetUserAddress", Name = "GetUserAddress")]
        public IActionResult GetUserAddress([FromQuery] string userId, [FromQuery] string state)
        {
            var query = new AddressByStateQuery{
                UserId = userId,
                State = state
            };
            var userProjection = new UserProjection(_userReadRepository);
            var address = userProjection.Handle(query);
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
            var userProjection = new UserProjection(_userReadRepository);
            var contact = userProjection.Handle(query);
            return Ok(contact);
        }
    }
}
