using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dapper_demo.DTOs.NoteDTOs
{
    public record NoteWriteDTO
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public int UserID { get; init; }
    }
}
