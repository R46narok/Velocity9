using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Xunit;
using ZeroGravity.Services.Authorization.Commands.Users.CreateUser;
using ZeroGravity.Services.Authorization.Dto;

namespace ZeroGravity.Tests.Integration.Authorization;

public class TokenControllerTests : IClassFixture<AuthorizationWebApplicationFactory>
{
   private readonly HttpClient _client;
   private readonly Faker<UserCredentialsDto> _faker;
   private readonly Faker<CreateUserCommand> _createFaker;
   
   public TokenControllerTests(AuthorizationWebApplicationFactory factory)
   {
      _client = factory.CreateClient();
      _faker = new Faker<UserCredentialsDto>()
         .RuleFor(x => x.UserName, f => f.Internet.UserName())
         .RuleFor(x => x.Password, f => f.Internet.Password(prefix: "#12"));
         
      _createFaker = new Faker<CreateUserCommand>()
         .RuleFor(x => x.UserName, f => f.Internet.UserName())
         .RuleFor(x => x.Email, f => f.Internet.Email())
         .RuleFor(x => x.Password, 
             f => f.Internet.Password(prefix: "#12"));
   }

   [Fact]
   public async Task CreateToken_ShouldReturnOk()
   {
      var command = _createFaker.Generate();
      
      var response = await _client.PostAsJsonAsync("/api/User", command);
      response.StatusCode.Should().Be(HttpStatusCode.OK);
      
      var credentials = new UserCredentialsDto { UserName = command.UserName, Password = command.Password};
      
      response = await _client.PostAsJsonAsync("/api/Token", credentials);
      response.StatusCode.Should().Be(HttpStatusCode.OK);
   }

   [Fact]
   public async Task CreateTokenWrongPassword_ShouldReturnUnauthorized()
   {
      var command = _createFaker.Generate();

      var response = await _client.PostAsJsonAsync("/api/User", command);
      response.StatusCode.Should().Be(HttpStatusCode.OK);

      var credentials = new UserCredentialsDto {UserName = command.UserName, Password = ""};

      response = await _client.PostAsJsonAsync("/api/Token", credentials);
      response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
   }

   [Fact]
   public async Task CreateTokenNonExistingUser_ShouldReturnUnauthorized()
   {
      var credentials = new UserCredentialsDto {UserName = "nonexisting", Password = "wrong"};

      var response = await _client.PostAsJsonAsync("/api/Token", credentials);
      response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

   }
}