using DevFreela.API.Models;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Commands.LoginUser;
using DevFreela.Application.InputModels;
using DevFreela.Application.Queries.GetUser;
using DevFreela.Application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMediator _mediator;

        public UsersController(IUserService userService, IMediator mediator)
        {
            _userService = userService;
            _mediator = mediator;
        }

        //api/users/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            //var user = _userService.GetUser(id);

            var userQuery = new GetUserQuery(id);

            var user = await _mediator.Send(userQuery);
            if (user is null) return NotFound();

            return Ok(user);
        }

        //api/users
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {
            //var id = _userService.Create(inputModel);           

            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = id }, command);
        }

        //api/users/login
        [HttpPut("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var loginUserViewModel = await _mediator.Send(command);
            if(loginUserViewModel is null) return BadRequest();

            return Ok(loginUserViewModel);
        }
    }
}
