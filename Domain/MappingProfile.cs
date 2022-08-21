using AutoMapper;
using Domain.DTO;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDto, ApplicationUser>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}
