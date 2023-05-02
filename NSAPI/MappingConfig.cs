using AutoMapper;
using NSAPI.Models;
using NSApp.Models;

namespace NSAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<MemberInterest, MemberInterestCreateDTO>().ReverseMap();
            CreateMap<MemberInterest, MemberInterestDTO>().ReverseMap();
            CreateMap<MemberInterest, MemberInterestUpdateDTO>().ReverseMap();
        }
    }
}
