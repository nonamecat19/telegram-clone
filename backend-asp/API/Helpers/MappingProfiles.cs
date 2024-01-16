using API.Dto;
using AutoMapper;
using Core.Entities;

namespace API.Helpers;

public class MappingProfiles : Profile
{
   public MappingProfiles()
   {
      CreateMap<User, UsersToReturnDto>();
      CreateMap<ChatMember, ChatMembersToReturnDto>()
         .ForMember(d => d.User, o => o.MapFrom(s => s.User.Name))
         .ForMember(d => d.ChatRoom, o => o.MapFrom(s => s.ChatRoom.Name));
   }
}