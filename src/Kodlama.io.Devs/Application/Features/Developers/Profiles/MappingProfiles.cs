using Application.Features.Developers.Commands.LoginDeveloper;
using Application.Features.Developers.Commands.RegisterDeveloper;
using Application.Features.Developers.Dtos;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Developers.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //CreateMap<RegisteredDeveloperDto,AccessToken>().ReverseMap();
            CreateMap<User, RegisterDeveloperCommand>().ReverseMap();
        }
    }
}
