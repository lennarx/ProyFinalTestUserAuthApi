using AutoMapper;
using Domain.Dtos;
using Domain.Forms;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Users.Commands;
using Services.Users.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public partial class UsersController : ControllerBase
    {

        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public UsersController(ILogger<UsersController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(ulong id)
        {
            var result = _mapper.Map<UserDto>(await _mediator.Send(new GetUserQuery(id), CancellationToken.None));

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser([FromBody] UserCreationForm userDto)
        {
            var user = _mapper.Map<UserDto>(await _mediator.Send(new CreateUserCommand(userDto), CancellationToken.None));
            return this.CreatedAtAction(nameof(this.Get), new { id = user.Id }, user);
        }
    }
}
