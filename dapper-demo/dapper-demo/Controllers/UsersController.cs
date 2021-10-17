using AutoMapper;
using dapper_demo.DTOs.UserDTOs;
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
        private readonly IMapper _mapper;
        public UsersController(ILogger<UsersController> logger, IUserRepository userRepository, IMapper mapper)
        {
            this._logger = logger;
            this._userRepository = userRepository;
            this._mapper = mapper;
        }

        [HttpGet]        
        [ProducesResponseType(typeof(IList<UserReadDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get() => Ok(_mapper.Map<List<UserReadDTO>> (await this._userRepository.GetUsers()));

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserReadDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id) {
            User user = await _userRepository.GetUserById(id);

            if(user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserReadDTO>(user));
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] UserWriteDTO userWriteDTO) {
            if (string.IsNullOrEmpty(userWriteDTO.Username))
                return BadRequest("Username is required.");
            await _userRepository.Create(_mapper.Map<User>(userWriteDTO));
            return Ok();
        }
    }
}
