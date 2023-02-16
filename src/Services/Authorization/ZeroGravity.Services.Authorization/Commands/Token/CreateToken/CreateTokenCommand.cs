using System.IdentityModel.Tokens.Jwt;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Authorization.Data.Entities;

namespace ZeroGravity.Services.Authorization.Commands.Token.CreateToken;

public class CreateTokenCommand : IRequest<PipelineResult<string>>
{
    public string UserName { get; set; } 
    public string Password { get; set; }
    
    public CreateTokenCommand(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }
}

public class CreateTokenCommandHandler : IRequestHandler<CreateTokenCommand, PipelineResult<string>>
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;

    private const int ExpirationInMinutes = 30;

    public CreateTokenCommandHandler(IConfiguration configuration, UserManager<User> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    public async Task<PipelineResult<string>> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
    {
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var user = await _userManager.FindByNameAsync(request.UserName);

        var claims = await _userManager.GetClaimsAsync(user);
        
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            expires: DateTime.UtcNow.AddMinutes(ExpirationInMinutes),
            claims: claims);

        var tokenHandler = new JwtSecurityTokenHandler();
        var stringToken = tokenHandler.WriteToken(token);

        return new(stringToken, "Successfully created a token");
    }
}