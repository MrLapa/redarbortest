using AutoMapper;
using Redarbor.Core.DTOs.Request;
using Redarbor.Core.Entities;
using System;

namespace Redarbor.Infrastructure.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<EmployeeRequestDto, Employee>()
                .ForMember(dest => dest.CreatedOn, orig => orig.MapFrom(src => Convert.ToDateTime(src.CreatedOn)))
                .ForMember(dest => dest.DeletedOn, orig => orig.MapFrom(src => Convert.ToDateTime(src.DeletedOn)))
                .ForMember(dest => dest.Lastlogin, orig => orig.MapFrom(src => Convert.ToDateTime(src.Lastlogin)))
                .ForMember(dest => dest.UpdatedOn, orig => orig.MapFrom(src => Convert.ToDateTime(src.UpdatedOn)))
                ;
        }
    }
}
