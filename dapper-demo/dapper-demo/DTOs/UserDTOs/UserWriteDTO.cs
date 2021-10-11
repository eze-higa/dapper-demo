using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dapper_demo.DTOs.UserDTOs
{
    public record UserWriteDTO
    {
        public string Username { get; init; }
    }
}
