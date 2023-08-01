using Api.Dto;
using Api.Entities;
using Api.Extensions;
using AutoMapper;

namespace Api.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //here we map from appuser to memeberDto
            //Dest is memberDto
            //scrc is appuser
            CreateMap<AppUser, MemberDto>()
            .ForMember(dest=>dest.PhotoUrl,opt=>opt.MapFrom(src=>src.Photos.FirstOrDefault(x=>x.IsMain).Url))
            .ForMember(dest=>dest.Age,opt=>opt.MapFrom(src=>src.DateOfBirth.CalculateAge()));;
            CreateMap<Photo, PhotoDto>();
        }
    }
}