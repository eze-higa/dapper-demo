using AutoMapper;
using dapper_demo.DTOs.NoteDTOs;
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
        private readonly IMapper _mapper;
        public NotesController(ILogger<NotesController> logger,INoteRepository noteRepository, IMapper mapper)
        {
            this._logger = logger;
            this._noteRepository = noteRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<NoteReadDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var notes = await _noteRepository.GetNotes();
            return Ok(_mapper.Map<IEnumerable<NoteReadDTO>>(notes));
        }

        [HttpGet("{id}", Name = "GetNoteById")]
        [ProducesResponseType(typeof(NoteReadDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetNoteById(int id)
        {
            var note = await _noteRepository.GetNoteById(id);

            if(note == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<NoteReadDTO>(note));
        }

        [HttpPost]
        [ProducesResponseType(typeof(NoteReadDTO), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] NoteWriteDTO noteDto)
        {
            Note note = _mapper.Map<Note>(noteDto);
            var createdNote = await _noteRepository.Create(note);

            return CreatedAtRoute(nameof(GetNoteById), new { id = note.Id }, _mapper.Map<NoteReadDTO>(createdNote));
        }

    }
}
