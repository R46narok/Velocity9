using AutoMapper;
using ZeroGravity.Services.Skeletal.Commands.CreateAuthor;
using ZeroGravity.Services.Skeletal.Data.Entities;
using ZeroGravity.Services.Skeletal.Data.Remotes;
using ZeroGravity.Services.Skeletal.Handlers;

namespace ZeroGravity.Services.Skeletal.Mapping;

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