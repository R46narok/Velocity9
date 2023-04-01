using AutoMapper;
using V9.Services.Authorization.Commands.Token.CreateToken;
using V9.Services.Authorization.Dto;

namespace V9.Services.Authorization.Mapping;

public class TokenMappingProfile : Profile
{
    public TokenMappingProfile()
    {
        CreateMap<UserCredentialsDto, CreateTokenCommand>();
    }
}