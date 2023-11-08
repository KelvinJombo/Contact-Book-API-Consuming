using AutoMapper;
using MyContactBookAPI.Models.Domain;
using MyContactBookAPI.Models.Dtos;

namespace MyContactBookAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<AddUserDto, User>().ReverseMap();

            CreateMap<UpdateUserDto, User>().ReverseMap();

            CreateMap<AddContactRequestDto, Contact>().ReverseMap();

            CreateMap<Contact, ContactDto>().ReverseMap();
            CreateMap<UpdateContactRequestDto, Contact>().ReverseMap();




        }
    }
}
