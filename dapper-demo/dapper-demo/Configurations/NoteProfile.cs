

using AutoMapper;
using dapper_demo.DTOs.NoteDTOs;
using dapper_demo.Entities;

namespace dapper_demo.Configurations
{
    public class NoteProfile: Profile
    {
        public NoteProfile()
        {
            CreateMap<NoteWriteDTO, Note>();
            CreateMap<Note, NoteReadDTO>();
        }
    }
}
