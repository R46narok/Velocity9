using AutoMapper;
using ZeroGravity.Services.Exercises.Commands.Authors.CreateAuthor;
using ZeroGravity.Services.Exercises.Commands.Authors.DeleteAuthor;
using ZeroGravity.Services.Exercises.Data.Entities;
using ZeroGravity.Services.Exercises.EventHandlers;

namespace ZeroGravity.Services.Exercises.Mapping;

public class AuthorMappingProfile : Profile
{
    public AuthorMappingProfile()
    {
        CreateMap<UserCreatedEvent, CreateAuthorCommand>()
            .ForMember(x => x.ExternalId, f => f.MapFrom(t => t.Id));
        CreateMap<CreateAuthorCommand, Author>();
        CreateMap<UserDeletedEvent, DeleteAuthorCommand>()
            .ForMember(x => x.ExternalId, f => f.MapFrom(t => t.Id));
    }
}