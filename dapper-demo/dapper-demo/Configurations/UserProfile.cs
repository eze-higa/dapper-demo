using AutoMapper;
using dapper_demo.DTOs.UserDTOs;
using dapper_demo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dapper_demo.Configurations
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<UserWriteDTO, User>();
            CreateMap<User, UserReadDTO>();
        }
    }
}
