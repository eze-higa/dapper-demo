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
    public class NotesController : ControllerBase
    {
        private readonly INoteRepository _noteRepository;
        private readonly ILogger<NotesController> _logger;
        public NotesController(ILogger<NotesController> logger,INoteRepository noteRepository)
        {
            this._logger = logger;
            this._noteRepository = noteRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Note>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _noteRepository.GetNotes());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] Note note)
        {
            await _noteRepository.Create(note);
            return Ok();
        }

    }
}
