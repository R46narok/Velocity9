using AutoMapper;
using ZeroGravity.Services.Skeletal.Commands.CreateAuthor;
using ZeroGravity.Services.Skeletal.Data.Entities;
using ZeroGravity.Services.Skeletal.Handlers;

namespace ZeroGravity.Services.Skeletal.Mapping;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<UserCreatedEvent, CreateAuthorCommand>()
                    .ForMember(x => x.ExternalId, f => f.MapFrom(t => t.Id));
        CreateMap<CreateAuthorCommand, Author>();
    }
}