using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dapper_demo.DTOs.UserDTOs
{
    public record UserReadDTO
    {
        public int Id { get; init; }
        public string Username { get; init; }
    }
}
