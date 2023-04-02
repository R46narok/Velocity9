using AutoMapper;
using V9.Services.Skeletal.Commands.CreateAuthor;
using V9.Services.Skeletal.Data.Entities;
using V9.Services.Skeletal.Data.Remotes;

namespace V9.Services.Skeletal.Mapping;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<RemoteUser, CreateAuthorCommand>()
            .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ReverseMap();
        
        CreateMap<CreateAuthorCommand, Author>();
    }
}