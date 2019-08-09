using AutoMapper;
using Praksa2.Repo.Models;
using Praksa2.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Praksa2.Service.Helpers
{
    /// <summary>
    /// Contains the mapping configuration used by the application, it enables mapping of user entities to dtos and dtos to entities.
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Map Users
            CreateMap<Users, UsersDto>();
            CreateMap<UsersDto, Users>();          
        }
    }
}
