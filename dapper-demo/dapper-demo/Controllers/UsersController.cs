using dapper_demo.Entities;
using dapper_demo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dapper_demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserRepository _userRepository;
        public UsersController(ILogger<UsersController> logger, IUserRepository userRepository)
        {
            this._logger = logger;
            this._userRepository = userRepository;
        }

        [HttpGet]        
        [ProducesResponseType(typeof(IList<User>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get() => Ok(await this._userRepository.GetUsers());

        [HttpGet("id/{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id) => Ok(await _userRepository.GetUserById(id));

        [HttpGet("username/{username}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByUsername(string username) => Ok(await _userRepository.GetUserByUsername(username));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] string username) {
            if (string.IsNullOrEmpty(username))
                return BadRequest("Username is required.");
            await _userRepository.Create(username);
            return Ok();
        }
    }
}
