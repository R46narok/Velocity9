using AutoMapper;
using ZeroGravity.Services.Authorization.Commands.Token.CreateToken;
using ZeroGravity.Services.Authorization.Dto;

namespace ZeroGravity.Services.Authorization.Mapping;

public class TokenMappingProfile : Profile
{
    public TokenMappingProfile()
    {
        CreateMap<UserCredentialsDto, CreateTokenCommand>();
    }
}