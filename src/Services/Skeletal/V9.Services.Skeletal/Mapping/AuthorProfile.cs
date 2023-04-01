using AutoMapper;
using V9.Services.Skeletal.Commands.CreateAuthor;
using V9.Services.Skeletal.Data.Entities;
using V9.Services.Skeletal.Data.Remotes;
using V9.Services.Skeletal.Handlers;

namespace V9.Services.Skeletal.Mapping;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<UserCreatedEvent, CreateAuthorCommand>()
                    .ForMember(x => x.ExternalId, f => f.MapFrom(t => t.Id)); 
        
        CreateMap<RemoteUser, CreateAuthorCommand>()
            .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ReverseMap();
        
        CreateMap<CreateAuthorCommand, Author>();
    }
}