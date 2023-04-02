using AutoMapper;
using V9.Services.Skeletal.Commands.CreateAuthor;
using V9.Services.Skeletal.Serverless.Authors.Queue;

namespace V9.Services.Skeletal.Serverless.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserCreatedEvent, CreateAuthorCommand>()
                    .ForMember(x => x.ExternalId, f => f.MapFrom(t => t.Id)); 
    }
}